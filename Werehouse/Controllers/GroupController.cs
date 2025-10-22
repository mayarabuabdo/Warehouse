using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;
using Warehouse.services;
namespace Warehouse.Controllers
{
    [Authorize(Policy = "Admin")]
    public class GroupController : Controller
    {
        IGroupServices GroupServices;
        public GroupController(IGroupServices groupServices)
        {
            GroupServices = groupServices;
        }

        public IActionResult AddGroupPage()
        {
            return View("AddGroup");
            
        }
        public async Task< IActionResult> AddGroup(GroupModel groupModel)
        {
            await  GroupServices.NewGroup(groupModel);
            ViewData["Active"] = true;
            List<GroupModel> groups = await GroupServices.GetActivatedGroups();
            return View("ViewGroup", groups);

        }
        public async Task< IActionResult> ViewActivatedGroups() {
            ViewData["Active"] = true;
            List< GroupModel > groups = await GroupServices.GetActivatedGroups();
            return View("ViewGroup", groups);

        }
        public async Task<IActionResult> ViewDeactivatedGroups()
        {
            ViewData["Active"] = false;
            List<GroupModel> groups = await GroupServices.GetDeactivatedGroups();
            return View("ViewGroup", groups);

        }
        public async Task<IActionResult> DeactivateGroup(int groupId)
        {
         
            await GroupServices.DeactiveGroup(groupId);
            ViewData["Active"] = false;
            List<GroupModel> groups = await GroupServices.GetDeactivatedGroups();
            return View("ViewGroup", groups);

        }

        public async Task<IActionResult> ActivateGroup(int groupId)
        {
            
            await GroupServices.ActiveGroup(groupId);
            ViewData["Active"] = true;
            List<GroupModel> groups = await GroupServices.GetActivatedGroups();
            return View("ViewGroup", groups);

        }

    }
}
