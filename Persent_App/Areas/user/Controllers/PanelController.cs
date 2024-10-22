using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Persent_App.Areas.user.Controllers
{
    [Area("user")]
    public class PanelController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
