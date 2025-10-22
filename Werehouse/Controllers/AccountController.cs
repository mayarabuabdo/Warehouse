using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Threading.Tasks;
using Warehouse.Models;
using Warehouse.services;

namespace Werehouse.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        IAccountService accountService;
        public AccountController(IAccountService _accountService) {
            accountService= _accountService;
        }

        public IActionResult LogInPage()
        {
            ViewData["InvalidInfo"] = false;
            UserLogin userLogin = new UserLogin();
            return View("LogIn", userLogin);
        }
        public async Task<IActionResult> LogIn(UserLogin userLogin)
        {
            ViewData["InvalidInfo"] = false;
         
           var result= await accountService.LogInProccess(userLogin);
            if (result) {
              
                return View("Home");
            }
            else
            {
                ViewData["InvalidInfo"] = true;
                return View("LogIn");
            }
        }
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.LogOutProccess(); 
            return RedirectToAction("LogInPage", "Account");
        }
    }
}
