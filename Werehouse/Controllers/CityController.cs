using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.services;

namespace Warehouse.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CityController : Controller
    {
        ICityServices CityServices;
        ICountryServices countryServices;
        public CityController(ICityServices _CityServices, ICountryServices _countryServices)
        {
            CityServices = _CityServices;
           countryServices = _countryServices;
        }
        public async Task<IActionResult> AddCityPage()
        {
            ViewData["Added"] = 0;
            ViewData["New"] = true;
            VMCity vMCity=new VMCity();
            vMCity.countries= await countryServices.GetActivatedCountry();
          vMCity.city= new City();
            return View("AddCityView", vMCity);
        }

        public async Task<IActionResult> AddCity(City city)
        {
            VMCity vMCity = new VMCity();
            int AddCityResult = await CityServices.NewCity(city);
    
            ViewData["New"] = true;
            ViewData["IsActive"] = true;
            vMCity.countries = await countryServices.GetActivatedCountry();

                vMCity.city = new City()
                {
                    Name = "",
                    PostalCode = "",
                    CountryId = 0
                };
            List<City> cities = await CityServices.GetActiveCities();
            return View("ViewCities", cities);


        }

        public async Task<IActionResult> ViewActivatedCities()
        {
            ViewData["IsActive"] = true;
            List<City> cities = await CityServices.GetActiveCities();
            return View("ViewCities", cities);
        }
        public async Task<IActionResult> ViewDeactivatedCities()
        {
            ViewData["IsActive"] = false;

            List<City> cities = await CityServices.GetdeactiveCities();
            return View("ViewCities", cities);
        }
        public async Task<IActionResult> DeactivateCity(int cityId)
        {
           
            await CityServices.DeactiveCity(cityId);
            ViewData["IsActive"] = false;
            List<City> cities = await CityServices.GetdeactiveCities();
            return View("ViewCities", cities);
        }
        public async Task<IActionResult> ActivateCity(int cityId)
        {
            await CityServices.activeCity(cityId);
            ViewData["IsActive"] = true;
            List<City> cities = await CityServices.GetActiveCities();
            return View("ViewCities", cities);
        }
        public async Task<IActionResult> EditCityPage(City city)
        {
            ViewData["Added"] = 0;
            ViewData["New"] = false;
            VMCity vMCity = new VMCity();
            vMCity.countries = await countryServices.GetActivatedCountry();
            vMCity.city = city;
            return View("AddCityView", vMCity);
        }
        public async Task<IActionResult> EditCity(City city)
        {
            
          await  CityServices.EditCityInfo(city);
            List<City> cities = await CityServices.GetActiveCities();
            return View("ViewCities", cities);
        }
    }
}
