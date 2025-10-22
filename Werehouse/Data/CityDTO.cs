using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        [ForeignKey("country")]
        public int CountryId { get; set; }
        public CountryDto country { get; set; }
        public List<WarehouseDTO> Warehouse { get; set; }
        public bool IsActive { get; set; }
      

    }
}
