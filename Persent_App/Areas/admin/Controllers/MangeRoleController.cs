using App.Services;
using Domin.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Persent_App.Areas.admin.Controllers
{
    [Area("admin")]
    public class MangeRoleController : Controller
    {
        private readonly IMangerRoleService _roleService;

        public MangeRoleController(IMangerRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(MangerRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError(string.Empty, "نام نقش الزامی است.");
                return View(model);
            }

            var result = await _roleService.CreateRoleAsync(model.Name);
            if (result == ResultRole.Success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "خطا در ایجاد نقش.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result == ResultRole.Success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "خطا در حذف نقش.");
            return View("Index");
        }
    }
}
