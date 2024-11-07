using Microsoft.AspNetCore.Mvc;

namespace Persent_App.Areas.admin.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Areas/admin/views/NavbarViewComponent.cshtml");
        }
    }
}
