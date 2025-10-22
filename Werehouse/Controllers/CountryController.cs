using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.services;
namespace Warehouse.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CountryController : Controller
    {
        ICountryServices countryServices;
        public CountryController(ICountryServices _countryServices) {

            countryServices= _countryServices;
        }  
        public async Task<IActionResult> ViewActivatedCountries()
        {
            ViewData["IsActivated"] = true;
            List<Country> Countries = await countryServices.GetActivatedCountry();
           return View("ViewCountry", Countries);   
        }
        public async Task<IActionResult> ViewDeactivatedCountries()
        {
            ViewData["IsActivated"] = false;
            List<Country> Countries = await countryServices.GetDeactivatedCountry();
            return View("ViewCountry", Countries);
        }
      /*  public async Task<IActionResult> AddCountry(Country country)
        {
            var userName = User.Identity?.Name;
            int added=await  countryServices.NewCountry(country, userName);
        
   
                Country emptyCountry = new Country()
                {
                    Name = "",
                    CountryCode = "",
                    PhoneCode = ""
                };
            ViewData["IsActivated"] = true;
            List<Country> Countries = await countryServices.GetActivatedCountry();
            return View("ViewCountry", Countries);
            return View("AddCountry", emptyCountry);
          
         
        }*/
        public async Task<IActionResult> AddCountrypages()
        {
            ViewData["New"] = true;
           
            Country country=new Country();
            return View("AddCountry", country);
        }
        public async Task<IActionResult> DeactivateCountry(int CountryId)
        {
            ViewData["IsActivated"] = false;
            countryServices.DeactivateCountry(CountryId);
            List<Country> Countries = await countryServices.GetDeactivatedCountry();
            return View("ViewCountry", Countries);
        }
        public async Task<IActionResult> ActivateCountry(int CountryId)
        {
            countryServices.ActivateCountry(CountryId);
            ViewData["IsActivated"] = true;
            List<Country> Countries = await countryServices.GetActivatedCountry();
            return View("ViewCountry", Countries);
        }

        public async Task<IActionResult> EditCountryPage(Country country)
        {
            ViewData["Added"] = 3;
            ViewData["New"] = false;
            return View("AddCountry", country);
        }

        public async Task<IActionResult> EditCountry(Country country)
        {

            await countryServices.EditCountryinfo(country);
            List<Country> Countries = await countryServices.GetActivatedCountry();
            return View("ViewCountry", Countries);
        }
    }
}
