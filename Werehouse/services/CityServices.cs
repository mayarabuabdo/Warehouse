using AutoMapper;
using System.Diagnostics.Metrics;
using Warehouse.Data;
using Warehouse.Data;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;
namespace Warehouse.services
{
    public class CityServices : ICityServices
    {
        WarehouseContext warehouseContext;
        IMapper mapper;
        public CityServices(WarehouseContext _warehouseContext, IMapper _mapper)
        {
            warehouseContext = _warehouseContext;
            mapper = _mapper;
        }

        public async Task<List<Models.City>> GetActiveCities()
        {
            List<Data.CityDTO> cityDTOs = warehouseContext.cities.Include(c => c.country).Where(c => c.IsActive==true).ToList();
            if (cityDTOs == null)
            {
                List<Models.City> city = new List<Models.City>();
                return city;
            }
            List<City> cities = cityDTOs.Select(c=> new City
            {
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode,
                CountryId = c.CountryId,
                CountryName = c.country.Name
            }
           ).ToList();


            return cities;

        }
        public async Task<List<Models.City>> GetdeactiveCities()
        {
            List<Data.CityDTO> cityDTOs = warehouseContext.cities.Include(c => c.country).Where(c => c.IsActive == false).ToList();
            if (cityDTOs == null)
            {
                List<Models.City> city = new List<Models.City>();
                return city;
            }
            List<City> cities = cityDTOs.Select(c => new City
            {
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode,
                CountryId = c.CountryId,
                CountryName = c.country.Name,
                CountryActiveStatus= c.country.IsActive

            }
           ).ToList();


            return cities;

        }
        public async Task<List<Models.City>> GetCities()
        {
            List<Data.CityDTO> cityDTOs = warehouseContext.cities.Include(c => c.country).ToList();
            if (cityDTOs == null)
            {
                List<Models.City> city = new List<Models.City>();
                return city;
            }
            List<City> cities = cityDTOs.Select(c => new City
            {
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode,
                CountryId = c.CountryId,
                CountryName = c.country.Name
            }
           ).ToList();


            return cities;

        }
        public async Task<int> NewCity(Models.City city)
        {
            bool isCityExist = warehouseContext.cities.Any(c => c.Name == city.Name
                                                      && c.PostalCode == city.PostalCode
                                                      && c.CountryId == city.CountryId);
            if (isCityExist)
            {
                return 1; 
            }

         if(city != null) {
                Data.CityDTO newCity = new Data.CityDTO
                {
                    Name = city.Name,
                    PostalCode = city.PostalCode,
                    CountryId = city.CountryId,
                    IsActive = true
                };

                warehouseContext.cities.Add(newCity);
                await warehouseContext.SaveChangesAsync();
            }
         

            return 2;

        }

        public async Task DeactiveCity(int cityId)
        {
            Data.CityDTO cityToDelete = warehouseContext.cities.Include(d => d.Warehouse).FirstOrDefault(c => c.Id == cityId);
            if (cityToDelete != null)
            {
                cityToDelete.Warehouse.ForEach(w => w.IsActive = false);

               cityToDelete.IsActive = false;
                warehouseContext.SaveChanges();
            }

        }
        public async Task activeCity(int cityId)
        {
            Data.CityDTO cityToDelete = warehouseContext.cities.Include(c => c.country).FirstOrDefault(c => c.Id == cityId);

            if (cityToDelete != null)
            {
                if (cityToDelete.country.IsActive == true) {
                    cityToDelete.IsActive = true;
                    warehouseContext.SaveChanges();

                }
               
            }

        }
        public async Task EditCityInfo(Models.City city)
        {
            Data.CityDTO cityToEdit = warehouseContext.cities.FirstOrDefault(c => c.Id == city.Id);
            if (cityToEdit != null)
            {
                cityToEdit.Name = city.Name;
                cityToEdit.PostalCode = city.PostalCode;
                cityToEdit.CountryId = city.CountryId;
        
                warehouseContext.SaveChanges();
            }
        }

        public async Task<List<Models.City>> GetCityInCountry(int countryId)
        {
            List<Data.CityDTO> allCities = warehouseContext.cities.Where(c=> c.CountryId == countryId && c.IsActive==true).ToList();
            List<Models.City> cityList = new List<Models.City>();
            if (allCities.Count != null)
            {
              foreach(Data.CityDTO c in allCities)
                {
                    Models.City city = new Models.City();
                    city.Name = c.Name;
                    city.PostalCode = c.PostalCode;
                    city.CountryId = c.CountryId;
                    city.Id = c.Id;
                    cityList.Add(city);
                }  
            }
            return cityList;
        }
       

    }
}