using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class WarehouseItemRequestDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? KUCode { get; set; }
        public int Qty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal? MSRPPrice { get; set; }
        [ForeignKey("warehouseDTO")]
        public int WarehouseId { get; set; }
        public WarehouseDTO warehouseDTO { get; set; }
        [ForeignKey(nameof(request))]
        public int RequestId { get; set; }
        public RequestDTO request { get; set; }
    }
}
