using Warehouse.Data;

namespace Warehouse.Models
{
    public class RequestDetailswithActionVM
    {
        public WarehouseItemsRequesDetailsModel RequestDetails { get; set; }
        public List<ActionModelcs> Actions { get; set; }
        public List<RequestDocumentLogModel> RequestDocuments { get; set; }
        public List<RequestDocumentLogModel> PreviousDocument { get; set; }
        public List<RequestLogModel> RequestLogs
        {
            get; set;

        }
    }
}
