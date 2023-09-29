using Microsoft.AspNetCore.Mvc;

namespace _3DeFI.API.Presentation;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("user")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDTO newUser)
    {
        
        return Ok();
    }
}