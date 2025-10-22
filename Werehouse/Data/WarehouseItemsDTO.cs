using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class WarehouseItemsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? KUCode { get; set; }
        public int Qty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal? MSRPPrice { get; set; }

        public List<WareHouseItemsRelation> WareHouseItemsRelation { get; set; }
        public bool IsActive { get; set; }
    }
}
