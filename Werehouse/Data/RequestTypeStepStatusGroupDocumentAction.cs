using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class RequestTypeStepStatusGroupDocumentAction
    {
        public int Id { get; set; }
        [ForeignKey("RequestType")]
        public int RequestTypeId { get; set; }
        public RequestType RequestType {  get; set; }
        [ForeignKey("Status")]    
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [ForeignKey("Step")]
        public int StepId { get; set; }
        public Step Step { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }


        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        public Document Document { get; set; }

        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        [ForeignKey("Action")]
        public int ActionId { get; set; }  
        public Action Action { get; set; }

    }
}
