using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.services;
namespace Warehouse.Controllers
{
    public class RequestController : Controller
    {
        IRequestServices requestServices;
        public RequestController(IRequestServices _requestServices)
        {
            requestServices= _requestServices;
        }
     
        public async Task<IActionResult> ViewUserRequest()
        {
            ViewData["PendingRequest"] = false;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<RequestModule> UserModel= await requestServices.GetRequestForUser(userId);
            return View("InboxRequest", UserModel);
        }
 
        public async Task<IActionResult> ViewWarehouseRequestDetials(RequestModule request)
        {
           List<RequestDocumentLogModel> requestDocuments = await  requestServices.GetRequestStepStatusAttachedDocument( request.Id);
            WarehouseItemsRequesDetailsModel requestDetails=await requestServices.GeetWarehouseRequestDetails(request.Id);
            List<RequestLogModel> requestLogs=  await requestServices.GetRequestLog(request.Id);
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<ActionModelcs> actions  =new List<ActionModelcs>();
            UserRequest userRequest= new UserRequest()
            {
                requestId = request.Id,
                userId=userId
            };
        
            List < ActionModelcs > RequestMandotoryAction = await requestServices.GetRequestDocumentActions(requestDocuments, userRequest);
            List<RequestDocumentLogModel> PreviousDocuments = await requestServices.GetPreviousAttachedDocument(request.Id);
            actions.AddRange(RequestMandotoryAction);
          
           
            RequestDetailswithActionVM requestDetailswithActionVM= new RequestDetailswithActionVM()
            {
                RequestDocuments= requestDocuments,
                Actions = actions,
                RequestDetails=requestDetails,
                PreviousDocument= PreviousDocuments,
                RequestLogs= requestLogs
                
            };

            return View("RequestDetails", requestDetailswithActionVM);

        }
  
        public async Task<IActionResult> ViewPendingRequest()
        {
            ViewData["PendingRequest"] = true;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<RequestModule> requests    =  await requestServices.GetPendingRequestInbox(userId);
            return View("InboxRequest", requests);
        }
        public async Task<IActionResult> ViewCancelRejectedConfirmedRequest()
        {
            ViewData["PendingRequest"] = false;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<RequestModule> requests = await requestServices.GetCancelRejectedConfirmedRequestInbox(userId);
            return View("InboxRequest", requests);
        }

        public async Task<IActionResult> UploadDocument(List<RequestDocumentLogModel> requestDocument , int requestId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            ViewData["DocumentSelected"] = true;
            if(requestDocument != null && requestDocument.Count > 0) {
              await  requestServices.saveDocument(requestDocument,userId);
            }
            ViewData["PendingRequest"] = true;

            return RedirectToAction("ViewRequstDocument", new { requestId = requestId });

        }
        public async Task<IActionResult> ViewRequstDocument(int requestId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserRequest userRequest = new UserRequest()
            {
                requestId = requestId,
                userId = userId
            };
            List<VMRequestDocument>  RequestDocuments =await  requestServices.GetAllRequsetStepStatusDocumentToAttach(userRequest);
            return View("DocumentView", RequestDocuments);
        }
        public async Task<IActionResult> DoAction(int requestId, int actionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserRequest userRequest = new UserRequest()
            {
                requestId = requestId,
                userId = userId
            };
            requestServices.DoRequestAction(userRequest, actionId);
            return RedirectToAction("ViewPendingRequest");
        }
        public async Task<IActionResult> DeleteRequestDocument(int requestDocumentId, int requestId)
        {
            requestServices.DeleteRequestDocument(requestDocumentId);
            return RedirectToAction("ViewRequstDocument", new { requestId = requestId });

        }
        public async Task<IActionResult> ViewPreviousRequestDocument(int requestId)
        {
          List<RequestDocumentLogModel>  PreviousDocuments= await requestServices.GetPreviousAttachedDocument(requestId);
            return View("PreviousDocumetView", PreviousDocuments);
        }
        public async Task<IActionResult> ViewRequestsLogs()
        {
         List<RequestLogModel> requestLog= await requestServices.GetAllRequestsLog();
            return View("RequestsLogs", requestLog);
        }
        public async Task<IActionResult> RequestSearchById(int requestId)
        {
            List<RequestLogModel> requestLogs = await requestServices.GetRequestLog(requestId);
            return View("RequestsLogs", requestLogs);
        }
    }
}
