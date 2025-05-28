using Microsoft.AspNetCore.Mvc;
using ShortenerApi.Models.DTOs;
using ShortenerApi.Services.Interfaces;

namespace ShortenerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinksController(ILinkService service) : ControllerBase
{
    [HttpPost("shorten")]
    public async Task<IActionResult> CreateShortUrlTask([FromBody] UrlRequest  request)
    {
        if(!Uri.IsWellFormedUriString(request.url, UriKind.Absolute)) return BadRequest();

        var shortUrl = await service.CreateShortLinkTask(request.url);
        
        return Ok(new
        {
            ShortUrl = shortUrl,
            OriginalUrl = request.url,
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetShortUrl()
    {
        var links = await service.GetAllLinksAsync();
        return Ok(links);
    }

    [HttpDelete("{shortCode}")]
    public async Task<IActionResult> DeleteShortUrl(string shortCode)
    {
        var success = await service.DeleteShortLinkAsync(shortCode);
        
        if(!success) return NotFound(new { message = "Short code not found" });
        
        return Ok(new { message = "Link deleted successfully" });
    }
    
}