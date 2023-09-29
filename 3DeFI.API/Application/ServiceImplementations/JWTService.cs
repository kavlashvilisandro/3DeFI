using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace _3DeFI.API.Application;

public class JWTService : IJWTService
{
    private readonly IConfiguration _config;
    public JWTService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateJWT(params Claim[] claims)
    {
        SymmetricSecurityKey symmetricKey = 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:PrivateKey")));
            
        SigningCredentials signingCredentials = 
            new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
            
        JwtSecurityToken token = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );
            
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}