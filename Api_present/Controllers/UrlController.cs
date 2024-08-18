using App.Services;
using Domin.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;
    private readonly ILogger<UrlController> _logger;

    public UrlController(IUrlService urlService, ILogger<UrlController> logger)
    {
        _urlService = urlService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlViewModel model)
    {
        _logger.LogInformation("Received request to create URL");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state");
            return BadRequest(ModelState);
        }

        var result = await _urlService.CreateUrl(model);
        _logger.LogInformation("Result from URL service: {Result}", result);

        switch (result)
        {
            case ResultUrl.Success:
                return Ok(new { success = true, message = "Operation completed successfully" });

            case ResultUrl.eror:
                return BadRequest(new { success = false, message = "An error occurred" });

            case ResultUrl.Duplicate:
                return Conflict(new { success = false, message = "Duplicate entry detected" });

            default:
                return StatusCode(500, new { success = false, message = "Unexpected result" });
        }
    }
}
