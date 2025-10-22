using Azure.Core;
using Warehouse.Models;

namespace Warehouse.services
{
    public interface IRequestServices
    {
        Task<WarehouseItemsRequesDetailsModel> GeetWarehouseRequestDetails(int requestId);
        Task<List<RequestModule>> GetRequestForUser(string userId);
        Task<List<RequestModule>> GetPendingRequestInbox(string userId);
        Task<List<RequestModule>> GetCancelRejectedConfirmedRequestInbox(string userId);
        Task<List<ActionModelcs>> GetRequestActions(RequestModule request,string userId);
        Task DoRequestAction(UserRequest userRequest, int actionId);
        Task saveDocument(List<RequestDocumentLogModel> tempDocument, string userId);
        Task<List<RequestDocumentLogModel>> GetPreviousAttachedDocument(int requestId);
        Task<List<RequestDocumentLogModel>> GetRequestStepStatusAttachedDocument(int requestId);
        Task<List<VMRequestDocument>> GetAllRequsetStepStatusDocumentToAttach(UserRequest userRequest);
        Task<ActionModelcs> GetCancelAction();
        Task<List<ActionModelcs>> GetRequestDocumentActions(List<RequestDocumentLogModel> requestDocuments, UserRequest userRequest);
        Task DeleteRequestDocument(int requestDocumentId);
        Task<List<RequestLogModel>> GetRequestLog(int requestId);
        Task<List<RequestLogModel>> GetAllRequestsLog();
    }
}