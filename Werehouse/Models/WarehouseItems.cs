using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class WarehouseItems
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? KUCode { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CountryId must be greater than 0")]
        public int Qty { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        public decimal? MSRPPrice { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public bool IsWarehouseActive { get; set; }
    }
}
