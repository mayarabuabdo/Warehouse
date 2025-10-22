using System.ComponentModel.DataAnnotations;

namespace Warehouse.Data
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List< CityDTO> city { get; set; }
     
        public string CountryCode { get; set; }
      
        public string PhoneCode { get; set; }
        public List<WarehouseDTO> Warehouse { get; set; }
        public bool IsActive { get; set; }
    }
}
