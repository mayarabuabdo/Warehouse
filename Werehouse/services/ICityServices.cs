using Warehouse.Models;

namespace Warehouse.services
{
    public interface ICityServices
    {
        Task EditCityInfo(City city);
        Task<List<City>> GetActiveCities();
        Task<List<City>> GetdeactiveCities();
        Task<int> NewCity(City city);
        Task activeCity(int cityId);
        Task DeactiveCity(int cityId);
        Task<List<City>> GetCityInCountry(int countryId);
    }
}