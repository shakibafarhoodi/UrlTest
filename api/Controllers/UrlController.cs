using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("Api/Url")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }
       
        public List<IActionResult> getAll()
        {
            return Ok(_urlService.GetAllUrlsAsync());
            
        }
       



    }
}
