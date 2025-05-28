using AdPlatformService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdPlatformService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly PlatformService _platformService;

    public PlatformsController(PlatformService platformService)
    {
        _platformService = platformService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл пустой дядя");

        var lines = new List<string>();

        using var reader = new StreamReader(file.OpenReadStream());
        while (!reader.EndOfStream)
        {
            lines.Add(await reader.ReadLineAsync() ?? string.Empty);
        }
        
        _platformService.LoadPlatforms(lines);

        return Ok();
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            return BadRequest("Локация не задана.");

        var result = _platformService.SearchPlatforms(location);
        return Ok(result);
    }
}