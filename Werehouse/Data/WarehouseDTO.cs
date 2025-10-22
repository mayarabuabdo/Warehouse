using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class WarehouseDTO
    {
        public int Id { get; set; }
  
        public string Name { get; set; }
       public  string Description { get; set; }
        [ForeignKey("Country")]
       public int CountryId { get; set; }
        public CountryDto Country { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public CityDTO City { get; set; }
        public List<WarehouseItemsDTO> warehouseItemsDTO { get; set; }
        public bool IsActive { get; set; }

    }
}
