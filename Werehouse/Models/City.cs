using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data;

namespace Warehouse.Models
{
    [AutoMap(typeof(Data.CityDTO), ReverseMap = true)]
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool CountryActiveStatus { get; set; }
    }
}
