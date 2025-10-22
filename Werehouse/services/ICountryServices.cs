using AutoMapper;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.services
{
    public interface ICountryServices
    {
        Task<List<Country>> GetActivatedCountry();
        Task<List<Country>> GetDeactivatedCountry();
        Task<int> NewCountry(Country country);
        Task<bool> EditCountryinfo(Country country);
        Task DeactivateCountry(int countryId);
        Task ActivateCountry(int countryId);
    }
}