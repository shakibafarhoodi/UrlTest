using Domin.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Persent_App.Areas.admin.Components
{
    public class MangeRoleViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("~/Areas/admin/views/MangeRolleComponent.cshtml");
        }
    }
}
