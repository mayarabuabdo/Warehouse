using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Models;

using Warehouse.services;

namespace Warehouse.Controllers
{
    [Authorize(Policy = "Manager")]
    public class WarehouseController : Controller
    {
        ICountryServices countryServices;
        ICityServices cityServices;
        IWarehouseService WarehouseService;
        public WarehouseController(ICityServices _cityServices, ICountryServices _countryServices, IWarehouseService _warehouseService)
        {
            cityServices = _cityServices;
            countryServices = _countryServices;
            WarehouseService = _warehouseService;
        }

        public async Task<IActionResult> AddWarehousestartPage()
        {
            ViewData["isCountrySelected"] = false;
            ViewData["SelectedCountry"] = 0;
            ViewData["AddWarehouse"] = true;
            WarehouseVM warehouseVM = new WarehouseVM();
            warehouseVM.countries = await countryServices.GetActivatedCountry();
            warehouseVM.warehouse = new WarehouseModel();
            warehouseVM.cities = new List<City>();
            return View("AddWarehouse", warehouseVM);
        }

        public async Task<IActionResult> AddWarehouseWithCityOptions(WarehouseModel warehouseModel)
        {
            ViewData["isCountrySelected"] = true;
            ViewData["SelectedCountry"] = warehouseModel.CountryId;
            ViewData["AddWarehouse"] = true;
            WarehouseVM warehouse = new WarehouseVM();
            warehouse.countries = await countryServices.GetActivatedCountry();
            warehouse.warehouse = warehouseModel;
            warehouse.cities = await cityServices.GetCityInCountry(warehouseModel.CountryId);

          

            return View("AddWarehouse", warehouse);
        }



        public async Task<IActionResult> AddWarehouse(WarehouseModel warehouseModel)
        {
       
            await WarehouseService.NewWarehouse(warehouseModel);
            ViewData["IsActive"] = true;
            List<WarehouseModel> Warehouses = await WarehouseService.GetActivatedWarehouses();
            return View("ViewWharehouse", Warehouses);
        }
  
       public async Task<IActionResult> ViewActivatedWherehouse()
        {
            ViewData["IsActive"] = true;
        List<WarehouseModel>  Warehouses=   await WarehouseService.GetActivatedWarehouses();
            return View("ViewWharehouse", Warehouses);
        }
        public async Task<IActionResult> ViewDeactivatedWherehouse()
        {
            ViewData["IsActive"] = false;
            List<WarehouseModel> Warehouses = await WarehouseService.GetDeactivedWarehouses();
            return View("ViewWharehouse", Warehouses);
        }
        public async Task<IActionResult> DeactivateWarehouse(int warehouseId)
        {
            await WarehouseService.DeactiveWarehouse(warehouseId);
            ViewData["IsActive"] = false;
            List<WarehouseModel> Warehouses = await WarehouseService.GetDeactivedWarehouses();
            return View("ViewWharehouse", Warehouses);
        }
        public async Task<IActionResult> ActivateWarehouse(int warehouseId)
        {
            await WarehouseService.ActiveWarehouse(warehouseId);
            ViewData["IsActive"] = true;
            List<WarehouseModel> Warehouses = await WarehouseService.GetActivatedWarehouses();
            return View("ViewWharehouse", Warehouses);
        }
        public async Task<IActionResult> EditWarehousePage(WarehouseModel warehouseModel)
        {
            ViewData["AddWarehouse"] = false;
            ViewData["isCountrySelected"] = true;
            ViewData["SelectedCountry"] = warehouseModel.CountryId;
            WarehouseVM warehouse = new WarehouseVM();
            warehouse.countries = await countryServices.GetActivatedCountry();
            warehouse.warehouse = warehouseModel;
            warehouse.cities = await cityServices.GetCityInCountry(warehouseModel.CountryId);
            return View("AddWarehouse", warehouse);

        }

        public async Task<IActionResult> EditWarehouseWithCityOptions(WarehouseModel warehouseModel)
        {
            ViewData["isCountrySelected"] = true;
            ViewData["SelectedCountry"] = warehouseModel.CountryId;
            ViewData["AddWarehouse"] = false;
            WarehouseVM warehouse = new WarehouseVM();
            warehouse.countries = await countryServices.GetActivatedCountry();
            warehouse.warehouse = warehouseModel;
            warehouse.cities = await cityServices.GetCityInCountry(warehouseModel.CountryId);
            return View("AddWarehouse", warehouse);
        }
        public async Task<IActionResult> EditWarehouse(WarehouseModel warehouseModel)
        {
          await  WarehouseService.EditWarehouseInfo(warehouseModel);
            ViewData["IsActive"] = true;
            List<WarehouseModel> Warehouses = await WarehouseService.GetActivatedWarehouses();
            return View("ViewWharehouse", Warehouses);

        }
    }
}
