using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Data;
using Warehouse.Models;
namespace Warehouse.services
{
    public class CountryServices : ICountryServices

    {
        WarehouseContext warehouseContext;
        IMapper mapper;
        public CountryServices(WarehouseContext _warehouseContext, IMapper _mapper)
        {
            mapper = _mapper;
            warehouseContext = _warehouseContext;
        }
        public async Task<int> NewCountry(Country country)
        {
            bool isExist = warehouseContext.countries.Any(c => c.Name == country.Name);
            if (isExist)
            {
                return 1;
            }
            else
            {
                CountryDto countryDto = new CountryDto()
                {
                    Name = country.Name,
                    PhoneCode = country.PhoneCode,
                    CountryCode = country.CountryCode,
                    IsActive=true
                };

                warehouseContext.countries.Add(countryDto);
                warehouseContext.SaveChanges();
                return 2;

            }
        }
        public async Task<List<Country>> GetActivatedCountry()
        {
         List<CountryDto> countries=  warehouseContext.countries.Where(c=>c.IsActive==true).ToList();
            List<Country> listCountries= mapper.Map<List<Country>>(countries);
            return listCountries;
        }
        public async Task<List<Country>> GetDeactivatedCountry()
        {
            List<CountryDto> countries = warehouseContext.countries.Where(c => c.IsActive == false).ToList();
            List<Country> listCountries = mapper.Map<List<Country>>(countries);
            return listCountries;
        }
        public async Task DeactivateCountry(int countryId)
        {
            CountryDto countryToDelete= warehouseContext.countries.Include(c=>c.city).FirstOrDefault(c=>c.Id==countryId);
           if(countryToDelete != null)
            {
                countryToDelete.IsActive = false;
                countryToDelete.city.ForEach(c => c.IsActive = false);
                foreach (var item in countryToDelete.Warehouse)
                {
                    item.IsActive = false;
                }

                warehouseContext.SaveChanges();
            }
          
        }
        public async Task ActivateCountry(int countryId)
        {
            CountryDto countryToDelete = warehouseContext.countries.FirstOrDefault(c => c.Id == countryId);
            if (countryToDelete != null)
            {
                countryToDelete.IsActive = true;
                warehouseContext.SaveChanges();
            }

        }
        public async Task<bool> EditCountryinfo(Country country)
        {
            CountryDto cuntryToUpdate = warehouseContext.countries.FirstOrDefault(c => c.Name == country.Name);
            if(cuntryToUpdate == null)
            {
                return false;
            }
            else
            {

                cuntryToUpdate.Name = country.Name;
                cuntryToUpdate.CountryCode = country.CountryCode;
                cuntryToUpdate.PhoneCode = country.PhoneCode;
                warehouseContext.SaveChanges();
                return true;
            }
        }

    }
}
