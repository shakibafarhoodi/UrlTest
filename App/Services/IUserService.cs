using Domin.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.ViewModel;

namespace App.Services
{
    public interface IUserService
    {
        //Task<List<ManageUserViewModel>> GetAllUsersAsync();
        List<ManageUserViewModel> GetAllUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(IdentityUser user);
        Task<IdentityResult> AddUserToRolesAsync(string userId, List<string> roles);
        Task<IdentityResult> RemoveUserFromRolesAsync(string userId, List<string> roles);
        Task<IdentityResult> DeleteUserAsync(string userId);
    }
}
