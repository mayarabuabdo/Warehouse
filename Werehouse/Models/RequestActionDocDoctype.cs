namespace Warehouse.Models
{
    public class VMRequestDocument
    {
        public int RequestDocumentId { get; set; }
        public string DocumentName { get; set; }
        public int DocumentId { get; set; }
        public string DocumentTypeName { get; set; }
        public int DocumentTypeId { get; set; }
        public int ActionId { get; set; }
        public int RequestId {  get; set; }
        public string? DocPath { get; set; }
        public int StepId { get; set; }
        public string StepName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }

    }
}
