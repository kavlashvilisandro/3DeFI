using _3DeFI.API.Application;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3DeFI.API.Presentation;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("user")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDTO newUser)
    {
        await _authService.AddNewUser(newUser.Adapt<NewUserRequestModel>());
        return Ok();
    }
    
    [HttpPost("user/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO newUser)
    {
        return Ok(await _authService.Login(newUser.Adapt<LoginUserRequestModel>()));
    }
}