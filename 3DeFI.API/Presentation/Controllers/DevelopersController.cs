using System.Net;
using _3DeFI.API.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3DeFI.API.Presentation;

[ApiController]
[Route("api/[controller]")]
public class DevelopersController : ControllerBase
{
    private readonly IDevelopersService _developersService;
    public DevelopersController(IDevelopersService developersService)
    {
        _developersService = developersService;
    }
    [HttpPost("project")]
    //[Authorize(AuthenticationSchemes = "UserAuth")]
    public async Task<IActionResult> UploadProject(IFormFile file)
    {
        await _developersService.UploadProject(file);
        return Ok();
    }

    [HttpGet("project")]
    //[Authorize(AuthenticationSchemes = "UserAuth")]
    public async Task<HttpResponseMessage> GetProjectByName(string fileName)
    {
        return await _developersService.GetProjectByName(fileName);
    }
}