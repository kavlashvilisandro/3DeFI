using System.Security.Claims;

namespace _3DeFI.API.Application;

public interface IJWTService
{
    public string GenerateJWT(params Claim[] claims);
}