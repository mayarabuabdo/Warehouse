using Microsoft.AspNetCore.Identity;
using Warehouse.Models;

namespace Warehouse.services
{
    public interface IAccountService
    {
        Task<bool> LogInProccess(UserLogin userLogin);
        Task LogOutProccess();
    }
}
