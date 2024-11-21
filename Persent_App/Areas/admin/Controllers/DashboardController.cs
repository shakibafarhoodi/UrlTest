using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Persent_App.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult CheckSession()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == null)
            {
                return Unauthorized(); // وضعیت سشن معتبر نیست
            }
            return Ok(); // وضعیت سشن معتبر است
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

    }
}
