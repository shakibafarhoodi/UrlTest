using Domin.InterFaceRepository;
using Domin.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class MangeRoleService : IMangerRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public MangeRoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ResultRole> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded ? ResultRole.Success : ResultRole.Error;
        }

        public async Task<ResultRole> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return ResultRole.Error;

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded ? ResultRole.Success : ResultRole.Error;
        }

        public async Task<List<MangerRoleViewModel>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(r => new MangerRoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }
    }
}
