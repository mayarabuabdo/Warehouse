using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data;

namespace Warehouse.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class WarehouseModel
    {
        public int Id { get; set; }
        [Required]
     
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        [Required]
        public int CityId { get; set; }
        public string CityName { get; set; }
   
    }
}
