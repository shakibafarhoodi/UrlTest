using Domin.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web.ViewModel;

namespace Persent_App.Areas.admin.Controllers
{
    [Area("admin")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;


        private readonly RoleManager<IdentityRole> _roleManager;
        //public ManageUserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        public ManageUsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]

        public IActionResult Index()
        {

            var model = _userManager.Users
                .Select(u => new ManageUserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,


                }).ToList();

            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> AddUserToRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var roles = _roleManager.Roles.ToList();
            var model = new AddUserToRoleViewModel() { UserId = id };

            foreach (var role in roles)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.UserRoles.Add(new UserRolesViewModel()
                    {
                        RoleName = role.Name
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await _userManager.AddToRolesAsync(user, requestRoles);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user); // گرفتن نقش‌های کاربر
            var model = new AddUserToRoleViewModel { UserId = id };

            foreach (var role in roles)
            {
                model.UserRoles.Add(new UserRolesViewModel
                {
                    RoleName = role,
                    IsSelected = true // نمایش نقش‌های کاربر به عنوان انتخاب‌شده
                });
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRoleViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.UserId)) return NotFound();

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            var rolesToRemove = model.UserRoles
                .Where(r => r.IsSelected)
                .Select(r => r.RoleName)
                .ToList();

            // حذف نقش‌ها از کاربر
            var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (result.Succeeded)
                return RedirectToAction("Index");

            // در صورت بروز خطا
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
        public IActionResult GetAllUser()
        {

            var model = _userManager.Users
                .Select(u => new ManageUserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,


                }).ToList();

            return View(model);
        }
    }
}
