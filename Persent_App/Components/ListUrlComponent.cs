using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persent_App.Components
{
    public class ListUrlComponent:ViewComponent
    {
        private readonly IUrlService _urlService;
        public ListUrlComponent(IUrlService urlService)
        {
            _urlService = urlService;
        }
        public async Task <IViewComponentResult> InvokeAsync()
        {
            var res = await _urlService.GetAllUrlsAsync();
            return View("~/Views/Url/ShowAllUrlComponents.cshtml", res);   
        }
    }
}
