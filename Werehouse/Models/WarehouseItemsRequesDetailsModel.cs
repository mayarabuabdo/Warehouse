using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data;

namespace Warehouse.Models
{
    [AutoMap(typeof(WarehouseItemRequestDetails), ReverseMap = true)]
    public class WarehouseItemsRequesDetailsModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string? KUCode { get; set; }
        public int Qty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal? MSRPPrice { get; set; }
       
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } 
   
        public int RequestId { get; set; }
        public string RequestTypeName { get; set; }

    }
}
