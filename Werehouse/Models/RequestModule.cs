using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data;
using AutoMapper;
namespace Warehouse.Models
{
    
    public class RequestModule
    {
       
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestTypeName { get; set; }
        public string RequestName { get; set; }
        public int StepId { get; set; }
        public string StepName { get; set; }
        public int StausId { get; set; }
        public string StatusName { get; set; }
        public string CreatedById { get; set; }
        public string CreatedName { get; set; }
        public string CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
