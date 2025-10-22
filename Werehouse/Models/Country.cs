using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Warehouse.Data;
namespace Warehouse.Models
{
    [AutoMap(typeof(CountryDto), ReverseMap = true)]
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string PhoneCode { get; set; }
    }
}
