using Domin.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IMangerRoleService
    {

        Task<ResultRole> CreateRoleAsync(string roleName);
            Task<ResultRole> DeleteRoleAsync(string roleId);
            Task<List<MangerRoleViewModel>> GetAllRolesAsync();
        
    }
}
