using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace Warehouse.Data
{
    public class RequestDTO
    {
        public int Id { get; set; }
   
        [ForeignKey(nameof(requestType))]    
        public int RequestTypeId { get; set; }
        public RequestType requestType { get; set; }
        [ForeignKey("step")]
        public int StepId { get; set; }
        public Step step { get; set; }

        [ForeignKey("status")]
        public int StausId { get; set; }
        public Status status { get; set; }
        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public WarehouseItemRequestDetails WarehouseItemRequestDetails { get; set; }  
        public bool IsActive { get; set; }
    }
}
