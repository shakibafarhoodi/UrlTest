using Microsoft.AspNetCore.Mvc;

namespace Persent_App.Controllers
{
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
