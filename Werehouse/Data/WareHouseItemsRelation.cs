using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class WareHouseItemsRelation
    {
        public int Id { get; set; } 
        [ForeignKey("warehouseDTO")]
        public int WarehouseId { get; set; }
        public WarehouseDTO warehouseDTO { get; set; }

        [ForeignKey("warehouseItemsDTO")]
        public int ItemId { get; set; }
        public WarehouseItemsDTO warehouseItemsDTO{ get; set; }
    }
}
