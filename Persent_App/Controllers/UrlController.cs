using App.Services;
using Domin.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persent_App.Views.Url;

namespace Persent_App.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<IActionResult> Index()
        {
            var urls = await _urlService.GetAllUrlsAsync();
            return View(urls);
        }
        [HttpGet]
        public async Task<IActionResult> GetContents()
        {
            var urls = await _urlService.GetAllUrlsAsync(); // متد دریافت تمام URLها و تصاویر

            var contents = urls.Select(url => new
            {
                type = string.IsNullOrEmpty(url.Img) ? "url" : "image",
                url = string.IsNullOrEmpty(url.Img) ? url.Url : Url.Content("~/Img/" + url.Img), // تنظیم مسیر تصاویر
                time = url.time
            }).ToList();
            return Json(contents);
        }

        [HttpGet]
        public IActionResult CreateUrl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUrl(CreateUrlViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _urlService.CreateUrl(model);

            switch (result)
            {
                case ResultUrl.Success:
                    return Json(new { success = true, message = "Operation completed successfully" });
                 
                case ResultUrl.eror:
                    return Json(new { success = false, message = "An error occurred" });
                  
                case ResultUrl.Duplicate:
                    return Json(new { success = false, message = "Duplicate entry detected" });

                case ResultUrl.Required:
                    return Json(new { success = false, message = "URL or ImgFile is required but not provided" });
            }
            return View(model);
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var url = await _urlService.GetUrlByIdAsync(id);
        //    if (url == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(url);
        //}

        [HttpPost]
        public async Task<IActionResult> UpdatePriorities([FromBody] List<UpdateUrlViewModel> updatedPriorities)
        {
            if (updatedPriorities == null || !updatedPriorities.Any())
            {
                return BadRequest("Invalid data.");
            }

            await _urlService.UpdatePrioritiesAsync(updatedPriorities);

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetUrls( )
        {
            var urls = await _urlService.GetAllUrlsAsync();
            return Json(urls);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteUrl([FromBody] int id)
        {
            _urlService.DeleteUrl(id);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult ListUrlComponent()
        {
            return ViewComponent("ListUrlComponent");
        }

     
    }
}

