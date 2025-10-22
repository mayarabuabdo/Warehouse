using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data;

namespace Warehouse.Models
{
    [AutoMap(typeof(RequestTypeStepStatusGroupDocumentAction), ReverseMap = true)]
    public class RequestTypeStepStatusGroupDocumentActionModle
    {
        public int Id { get; set; }
      
        public int RequestTypeId { get; set; }

       
        public int StatusId { get; set; }
 
        public int StepId { get; set; }

        public int GroupId { get; set; }
     
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int DocumentTypeId { get; set; }

        public int ActionId { get; set; }

    }
}
