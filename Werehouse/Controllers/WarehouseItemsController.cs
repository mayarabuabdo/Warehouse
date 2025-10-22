using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Security.Claims;
using Warehouse.Models;
using Warehouse.services;

namespace Warehouse.Controllers
{
    [Authorize(Policy = "Employee")]
    public class WarehouseItemsController : Controller
    {
        IWarehouseService warehouseService;
        IRequestServices requestServices;
        IWarehouseItemsServices WarehouseItemsServices { get; set; }
        public WarehouseItemsController(IWarehouseService _warehouseService, IWarehouseItemsServices _warehouseItemsServices, IRequestServices requestServices)
        {
            warehouseService = _warehouseService;
            WarehouseItemsServices = _warehouseItemsServices;
            this.requestServices = requestServices;
        }


        public async Task<IActionResult> AddWarehoueItemsPage()
        {
            ViewData["isNew"] = true;
            WarehouseItemsVm warehouseItemsVm=new WarehouseItemsVm();
            warehouseItemsVm.warehouseItems = new WarehouseItems();
            warehouseItemsVm.warehouseModel = await warehouseService.GetActivatedWarehouses();

            return View("AddWarehoueItems", warehouseItemsVm);
        }

        /*  public async Task<IActionResult> AddWarehoueItems(WarehouseItems warehouseItems)
          {
              ViewData["isNew"] = true;
              await WarehouseItemsServices.AddItems(warehouseItems);
              WarehouseItemsVm warehouseItemsVm = new WarehouseItemsVm();
              warehouseItemsVm.warehouseItems = warehouseItems;
              warehouseItemsVm.warehouseModel = await warehouseService.GetWarehouses();
              List<WarehouseItems> warehouseItemsList = await WarehouseItemsServices.GetItems();
              return View("ViewWarehouseItems", warehouseItemsList);
          }
        */
      public async Task<IActionResult> AddWarehoueItems(WarehouseItems warehouseItems)
        {
            var username = User.Identity?.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

           await WarehouseItemsServices.AddItems(warehouseItems, username);
          List<RequestModule>  userRequest   = await requestServices.GetPendingRequestInbox(userId);
            ViewData["PendingRequest"] = true;
            return View("~/Views/Request/InboxRequest.cshtml", userRequest);
        }

        public async Task<IActionResult> ViewActivatedWarehouseItems()
        {
            ViewData["IsActive"] = true;
            List<WarehouseItems> warehouseItems = await WarehouseItemsServices.GetActivatedItems();
            return View("ViewWarehouseItems", warehouseItems);
        }
        public async Task<IActionResult> ViewDeactivatedWarehouseItems()
        {
            ViewData["IsActive"] = false;
            List<WarehouseItems> warehouseItems = await WarehouseItemsServices.GetDeactivatedItems();
            return View("ViewWarehouseItems", warehouseItems);
        }

        public async Task<IActionResult> DeactivateWarehouseItem(int warehouseId)
        {
          
            await WarehouseItemsServices.DeactiveWarehouseItem(warehouseId);
            ViewData["IsActive"] = false;
            List<WarehouseItems> warehouseItems = await WarehouseItemsServices.GetDeactivatedItems();
            return View("ViewWarehouseItems", warehouseItems);
        }
        public async Task<IActionResult> ActivateWarehouseItem(int warehouseId)
        {
            await WarehouseItemsServices.ActiveWarehouseItem(warehouseId);
            ViewData["IsActive"] = true;
            List<WarehouseItems> warehouseItems = await WarehouseItemsServices.GetActivatedItems();
            return View("ViewWarehouseItems", warehouseItems);
        }
        public async Task<IActionResult> EditWarehousePage(WarehouseItems warehouseItems)
        {
            ViewData["isNew"] = false;
            WarehouseItemsVm warehouseItemsVm = new WarehouseItemsVm();
            warehouseItemsVm.warehouseItems = warehouseItems;
            warehouseItemsVm.warehouseModel = await warehouseService.GetActivatedWarehouses();

            return View("AddWarehoueItems", warehouseItemsVm);
        }
        public async Task<IActionResult> EidtWarehouseItem(WarehouseItems warehouseItems)
        {
            await WarehouseItemsServices.EditWarehouseItem(warehouseItems);
            List<WarehouseItems> warehouseItem = await WarehouseItemsServices.GetActivatedItems();
            return View("ViewWarehouseItems", warehouseItem);
        }
      
    }
}
