using AutoMapper;
using Azure.Core;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Warehouse.Data;
using System.IO;
using Warehouse.Models;
namespace Warehouse.services
{
    public class RequestServices : IRequestServices
    {
        WarehouseContext warehouseContext;
        IMapper mapper;
        public RequestServices(WarehouseContext _warehouseContext, IMapper _mapper)
        {
            warehouseContext = _warehouseContext;
            mapper = _mapper;
        }
        public async Task<List<RequestModule>> GetRequestForUser(string userId)
        {
            List<RequestDTO> requests = warehouseContext.Requests.Where(r => r.CreatedById == userId).Include(r => r.requestType).
                     Include(r => r.step).Include(r => r.status).Include(r => r.CreatedBy).ToList();

            List<RequestModule> userRequests = new List<RequestModule>();

            foreach (RequestDTO request in requests)
            {
                RequestModule requestModule = new RequestModule()
                {
                    Id = request.Id,
                    CreatedById = request.CreatedById,
                    CreatedName = request.CreatedBy.UserName,
                    CreatedAt = request.CreatedAt,
                    StausId = request.StausId,
                    StatusName = request.status.Name,
                    StepId = request.StepId,
                    StepName = request.step.Name,
                    IsActive = request.IsActive,
                    RequestTypeId = request.RequestTypeId,
                    RequestTypeName = request.requestType.Name
                };

                userRequests.Add(requestModule);
            }

            return userRequests;
        }

        public async Task<WarehouseItemsRequesDetailsModel> GeetWarehouseRequestDetails(int requestId)
        {
            WarehouseItemsRequesDetailsModel warehouseDetails = new WarehouseItemsRequesDetailsModel();
            if (requestId != null)
            {
                WarehouseItemRequestDetails warehouseItemsRequesDetails = warehouseContext.WarehouseItemRequestDetails.FirstOrDefault(w => w.RequestId == requestId);
                if (warehouseItemsRequesDetails != null)
                {
                    warehouseDetails = mapper.Map<WarehouseItemsRequesDetailsModel>(warehouseItemsRequesDetails);
                    warehouseDetails.WarehouseName = warehouseContext.warehouses.Where(w => w.Id == warehouseItemsRequesDetails.WarehouseId).Select(w => w.Name).FirstOrDefault();
                    warehouseDetails.RequestTypeName = warehouseContext.Requests.Where(r => r.Id == requestId).Include(r => r.requestType).Select(r => r.requestType.Name).FirstOrDefault();
                }

            }
            return warehouseDetails;

        }
        public async Task<List<RequestModule>> GetRequest(List<RequestTypeStepStatusGroup> requestTypeStepStatusGroups)
        {
            List<RequestModule> Requests = new List<RequestModule>();
            foreach (var RTSSG in requestTypeStepStatusGroups)
            {
                List<RequestDTO> Requstes = warehouseContext.Requests.Where(r => r.StausId == RTSSG.StatusId && r.StepId == RTSSG.StepId && r.RequestTypeId == RTSSG.RequestTypeId).
                    Include(r => r.status).Include(r => r.CreatedBy).Include(r => r.requestType).Include(r => r.step).ToList();
                foreach (RequestDTO request in Requstes)
                {
                    RequestModule requestModule = new RequestModule()
                    {
                        Id = request.Id,
                        StepId = request.StepId,
                        StausId = request.StausId,
                        CreatedAt = request.CreatedAt,
                        CreatedById = request.CreatedById,
                        StepName = request.step.Name,
                        StatusName = request.status.Name,
                        CreatedName = request.CreatedBy.UserName,
                        RequestTypeName = request.requestType.Name,
                        RequestTypeId = request.RequestTypeId

                    };
                    Requests.Add(requestModule);
                }
            }
            return Requests;
        }
        public async Task<List<RequestModule>> GetPendingRequestInbox(string userId)
        {
            List<int> groupsId = warehouseContext.UserGroup.Where(u => u.UserId == userId).Select(u => u.GroupId).ToList();
            List<RequestModule> Requests = new List<RequestModule>();
            if (groupsId != null && groupsId.Count != 0)
            {
                foreach (int id in groupsId)
                {
                    int PendinstatusId = warehouseContext.Status.Where(s => s.Name == "Pending").Select(s => s.Id).FirstOrDefault();
                    List<RequestTypeStepStatusGroup> requestTypeStepStatusGroups = warehouseContext.RequestTypeStepStatusGroup.Where(R => R.GroupId == id && R.StatusId == PendinstatusId).ToList();
                    Requests = await GetRequest(requestTypeStepStatusGroups);


                }

            }
            return Requests;

        }
        public async Task<List<RequestModule>> GetCancelRejectedConfirmedRequestInbox(string userId)
        {
            List<int> groupsId = warehouseContext.UserGroup.Where(u => u.UserId == userId).Select(u => u.GroupId).ToList();
            List<RequestModule> Requests = new List<RequestModule>();
            if (groupsId != null && groupsId.Count != 0)
            {
                foreach (int id in groupsId)
                {
                    int CanceledstatusId = warehouseContext.Status.Where(s => s.Name == "Canceled").Select(s => s.Id).FirstOrDefault();
                    int ConfirmedstatusId = warehouseContext.Status.Where(s => s.Name == "Confirmed").Select(s => s.Id).FirstOrDefault();
                    int RejectedstatusId = warehouseContext.Status.Where(s => s.Name == "Rejected").Select(s => s.Id).FirstOrDefault();
                    List<RequestTypeStepStatusGroup> requestTypeStepStatusGroups = warehouseContext.RequestTypeStepStatusGroup.Where(R => R.GroupId == id && (R.StatusId == CanceledstatusId || R.StatusId == ConfirmedstatusId || R.StatusId == RejectedstatusId)).ToList();
                    Requests = await GetRequest(requestTypeStepStatusGroups);


                }

            }
            return Requests;

        }

        public async Task<List<ActionModelcs>> GetRequestActions(RequestModule request, string userId)
        {
            List<int> groupIds = warehouseContext.UserGroup.Where(r => r.UserId == userId).Select(r => r.GroupId).ToList();
            List<ActionModelcs> actions = new List<ActionModelcs>();

            foreach (int id in groupIds)
            {

                List<Warehouse.Data.Action> userActions = warehouseContext.RequestTypeGroupStatusStepAction.Where(r => r.GroupId == id && r.StepId == request.StepId && r.StatusId == request.StausId && r.RequestTypeId == request.RequestTypeId).
                    Include(r => r.Action).Select(r => r.Action).ToList();
                List<ActionModelcs> actionModel = mapper.Map<List<ActionModelcs>>(userActions);
                actions.AddRange(actionModel);
            }



            return actions;
        }
        public async Task CreateNewWarehouseItem(WarehouseItemRequestDetails warehouseItem)
        {
            WarehouseItemsDTO warehouseItemToadd = new WarehouseItemsDTO()
            {
                KUCode = warehouseItem.KUCode,
                CostPrice = warehouseItem.CostPrice,
                Name = warehouseItem.Name,
                MSRPPrice = warehouseItem.MSRPPrice,
                Qty = warehouseItem.Qty,
                IsActive = true
            };

            warehouseContext.WarehouseItemsDTO.Add(warehouseItemToadd);
            warehouseContext.SaveChanges();
            int newItemId = warehouseItemToadd.Id;
            WareHouseItemsRelation newRelation = new WareHouseItemsRelation()
            {
                WarehouseId = warehouseItem.WarehouseId,
                ItemId = newItemId
            };

            warehouseContext.wareHouseItemsRelation.Add(newRelation);

            warehouseContext.SaveChanges();
        }
       
        public async Task saveDocument(List<RequestDocumentLogModel> tempDocument, string userId)
        {
           foreach (RequestDocumentLogModel requestDocument in tempDocument)
            {
                if(requestDocument.DocumentFile != null) {
                  bool isDocAttached = warehouseContext.RequestDocumentLog.Any(r => r.RequestId == requestDocument.RequestId && r.DocumentId == requestDocument.DocumentId && r.StepId==requestDocument.StepId && r.StatusId==requestDocument.StatusId);
                    if(!isDocAttached ) {

                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "temp");
                        string fileName = Path.GetFileName(requestDocument.DocumentFile.FileName); 
                        string filePath = Path.Combine(folderPath, fileName);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(requestDocument.DocumentFile.FileName);
                            string extension = Path.GetExtension(requestDocument.DocumentFile.FileName);
                            fileName = $"{fileNameWithoutExt}{counter}{extension}";
                            filePath = Path.Combine(folderPath, fileName);
                            counter++;
                        }

                        requestDocument.DocumentFile.CopyTo(new FileStream(@$"wwwroot\temp\{fileName}", FileMode.Create));


                        requestDocument.Extension = @$"/temp/{fileName}";
                        requestDocument.CreatedAt= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        requestDocument.CreatedById = userId;

                        RequestDocumentLog requestDocumentNew = mapper.Map<RequestDocumentLog>(requestDocument);
                     
                        warehouseContext.RequestDocumentLog.Add(requestDocumentNew);
                        warehouseContext.SaveChanges();
                    }
                

                }
             
            }
       
        }
        public async Task DoRequestAction(UserRequest userRequest, int actionId)
        {
            RequestDTO request = warehouseContext.Requests.Where(r => r.Id == userRequest.requestId).Include(r => r.requestType).FirstOrDefault();
            string actionName = warehouseContext.Actions.Where(r => r.Id == actionId).Select(r => r.Name).FirstOrDefault();
            
            if (actionName == "Confirm")
            {
                WarehouseItemRequestDetails warehouseItem = warehouseContext.WarehouseItemRequestDetails.FirstOrDefault(w => w.RequestId == userRequest.requestId);

                CreateNewWarehouseItem(warehouseItem);

            }
            int currentStepId = request.StepId;
            int currentStatusId = request.StausId;
            int trackId = request.requestType.TrackId;


            TrackStep trackStep = warehouseContext.TrackStep.FirstOrDefault(t => t.TrackId == trackId && t.CurrentStepId == currentStepId && t.CurrentStatusId == currentStatusId && t.ActionId == actionId);
            request.StepId = trackStep.NextStepId;
            request.StausId = trackStep.NextStatusId;
            warehouseContext.SaveChanges();
            string actionTokentime= DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            RequestLog requestLog = new RequestLog()
            {
                StepId = currentStepId,
                StatusId = currentStatusId,
                ActionId=actionId,
                RequestId= userRequest.requestId,
                ActionTokenAt=actionTokentime,
                ActionTokenById= userRequest.userId

            };   

            warehouseContext.RequestLog.Add(requestLog);
            warehouseContext.SaveChanges();
        }
        public async Task<List<VMRequestDocument>> GetAllRequsetStepStatusDocumentToAttach(UserRequest userRequest)
        {
            RequestDTO request= warehouseContext.Requests.FirstOrDefault(r => r.Id == userRequest.requestId);
            List<int> userGroupIds=  warehouseContext.UserGroup.Where(u => u.UserId == userRequest.userId).Select(u=>u.GroupId).ToList();
            List<VMRequestDocument> RequestStepStatusDocument = new List<VMRequestDocument>();
            if (request != null && userGroupIds !=null && userGroupIds.Count>0) {
                foreach(int groupId in userGroupIds) {

                    List<RequestTypeStepStatusGroupDocumentAction> requestTypeStepStatusGroupDocumentActions = warehouseContext.RequestTypeStepStatusGroupDocumentAction.Where(r => r.RequestTypeId == request.RequestTypeId && r.StatusId == request.StausId && r.StepId == request.StepId && r.GroupId==groupId).
            Include(r => r.Document).Include(r => r.DocumentType).Include(r => r.Status).Include(r => r.Step).ToList();

                    foreach (var requsetDocument in requestTypeStepStatusGroupDocumentActions)
                    {
                        var requestDocPath = warehouseContext.RequestDocumentLog.Where(r => r.RequestId == request.Id && r.DocumentId == requsetDocument.DocumentId && r.StatusId == requsetDocument.StatusId && r.StepId == requsetDocument.StepId).FirstOrDefault();
                        VMRequestDocument requestActionDocDoctype = new VMRequestDocument()
                        {
                            DocumentId = requsetDocument.DocumentId,
                            DocumentName = requsetDocument.Document.Name,
                            DocumentTypeId = requsetDocument.DocumentTypeId,
                            DocumentTypeName = requsetDocument.DocumentType.Name,
                            ActionId = requsetDocument.ActionId,
                            RequestId = request.Id,
                            StepId = requsetDocument.StepId,
                            StatusId = requsetDocument.StatusId,
                            StepName = requsetDocument.Step.Name,
                            StatusName = requsetDocument.Status.Name
                        };

                        if (requestDocPath != null)
                        {
                            requestActionDocDoctype.DocPath = requestDocPath.Extension;
                            requestActionDocDoctype.RequestDocumentId = requestDocPath.Id;
                        }
                        RequestStepStatusDocument.Add(requestActionDocDoctype);

                    }


                }

            }
     
            return RequestStepStatusDocument;
        }
       
        public async Task<List<RequestDocumentLogModel>> GetRequestStepStatusAttachedDocument(int requestId)
        {
            RequestDTO request = warehouseContext.Requests.FirstOrDefault(r => r.Id == requestId);
            List<RequestDocumentLogModel> requestDocumentLogModels = new List<RequestDocumentLogModel>();
            List<RequestDocumentLog> requestDocuments = warehouseContext.RequestDocumentLog.Where(r=>r.RequestId== requestId && r.StatusId==request.StausId && r.StepId==request.StepId).
                Include(r=>r.Status).Include(r=>r.Step).Include(r=>r.CreatedBy).Include(r=>r.document).ToList();
            requestDocumentLogModels = mapper.Map<List<RequestDocumentLogModel>>(requestDocuments);
            return (requestDocumentLogModels);
        }

        public async Task<List<RequestDocumentLogModel>> GetPreviousAttachedDocument(int requestId)
        {
            RequestDTO request = warehouseContext.Requests.FirstOrDefault(r => r.Id == requestId);
            List<RequestDocumentLogModel> requestDocumentLogModels = new List<RequestDocumentLogModel>();
            List<RequestDocumentLog> requestDocuments = new List<RequestDocumentLog>();
            int pendingStatusId = warehouseContext.Status.Where(s=>s.Name== "Pending").Select(s=>s.Id).FirstOrDefault();
            if (request.StausId == pendingStatusId) {
                requestDocuments = warehouseContext.RequestDocumentLog.Where(r => r.RequestId == requestId && r.StepId != request.StepId).Include(r => r.Status).Include(r => r.Step).Include(r => r.CreatedBy).Include(r => r.document).ToList();
               
            }
            else
            {
                requestDocuments = warehouseContext.RequestDocumentLog.Where(r => r.RequestId == requestId ).Include(r => r.Status).Include(r => r.Step).Include(r => r.CreatedBy).Include(r => r.document).ToList();
            }
                requestDocumentLogModels = mapper.Map<List<RequestDocumentLogModel>>(requestDocuments);
            return (requestDocumentLogModels);
        }

        public async Task<List<ActionModelcs>> GetRequestDocumentActions(List<RequestDocumentLogModel> requestDocuments, UserRequest userRequest)
        {
            RequestDTO request = warehouseContext.Requests.FirstOrDefault(r => r.Id == userRequest.requestId);


            int MandatoryDocId = warehouseContext.DocumentType.Where(r => r.Name == "Mandatory").Select(r => r.Id).FirstOrDefault();
            List<ActionModelcs> actions = new List<ActionModelcs>();
            List<int> actionIdsForAttachedDocument = new List<int>();
            List<int> groupIds = warehouseContext.UserGroup.Where(u => u.UserId == userRequest.userId).Select(u => u.GroupId).ToList();
            List<int> requestDocumentIds = requestDocuments.Select(r => r.DocumentId).ToList();
            foreach (var id in groupIds)
            {
                List<int> actionIds = warehouseContext.RequestTypeGroupStatusStepAction.Where(r => r.RequestTypeId == request.RequestTypeId && r.GroupId == id && r.StatusId == request.StausId && r.StepId == request.StepId).Select(r => r.ActionId).ToList();

                foreach (int actionId in actionIds)
                {
                    List<int> documentIds = warehouseContext.RequestTypeStepStatusGroupDocumentAction.Where(r => r.RequestTypeId == request.RequestTypeId && r.GroupId == id && r.StatusId == request.StausId && r.StepId == request.StepId && r.ActionId == actionId && r.DocumentTypeId == MandatoryDocId).Select(r => r.DocumentId).ToList();
                    bool isThereMissing = documentIds.Except(requestDocumentIds).Any();

                    if (!isThereMissing)
                    {
                        Warehouse.Data.Action action = warehouseContext.Actions.FirstOrDefault(a => a.Id == actionId);
                        ActionModelcs actionModelcs = mapper.Map<ActionModelcs>(action);


                        actions.Add(actionModelcs);
                    }
                }

            }



            return actions;
        }

        public async Task<ActionModelcs> GetCancelAction()
        {
            Warehouse.Data.Action   action  = warehouseContext.Actions.FirstOrDefault(a=>a.Name=="Cancel");
            ActionModelcs actionModelcs= new ActionModelcs() { Name=action.Name,
            Id=action.Id,
            ButtonStyle=action.ButtonStyle,
            IsActive=action.IsActive};
            return actionModelcs;
        }
      
        public async Task DeleteRequestDocument(int requestDocumentId)
        {
           RequestDocumentLog requestDoc=warehouseContext.RequestDocumentLog.FirstOrDefault(r => r.Id == requestDocumentId);
            if (requestDoc != null)
            {
                warehouseContext.RequestDocumentLog.Remove(requestDoc);
                warehouseContext.SaveChanges();
            }

        }
        public async Task<List<RequestLogModel>> GetRequestLog(int requestId)
        {
         List<RequestLog> requestLogs = warehouseContext.RequestLog.Where(r => r.RequestId == requestId).Include(r=>r.Step).Include(r=>r.Status).Include(r=>r.Action).Include(r=>r.ActionTokenBy).ToList();
            List<RequestLogModel> requestLogModels= new List<RequestLogModel>();    
            if (requestLogs.Count > 0 && requestLogs !=null) {
             requestLogModels = mapper.Map<List<RequestLogModel>>(requestLogs);
            }
           return requestLogModels;
        }

        public async Task<List<RequestLogModel>> GetAllRequestsLog()
        {
            List<RequestLog> requestLogs = warehouseContext.RequestLog.Include(r => r.Step).Include(r => r.Status).Include(r => r.Action).Include(r => r.ActionTokenBy).ToList();
            List<RequestLogModel> requestLogModels = new List<RequestLogModel>();
            if (requestLogs.Count > 0 && requestLogs != null)
            {
                requestLogModels = mapper.Map<List<RequestLogModel>>(requestLogs);
            }
            return requestLogModels;
        }



    }
}