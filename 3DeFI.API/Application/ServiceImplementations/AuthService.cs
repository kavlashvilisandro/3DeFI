using System.Security.Claims;
using _3DeFI.API.Domain;
using _3DeFI.API.Infrastructure;
using Mapster;
using Npgsql;

namespace _3DeFI.API.Application;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _config;
    private readonly IJWTService _jwtService;
    public AuthService(IAuthRepository authRepository, IConfiguration config, IJWTService jwtService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
        _config = config;
    }
    public async Task AddNewUser(NewUserRequestModel newUser)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_config.GetConnectionString("Default")))
        {
            await connection.OpenAsync();
            using(NpgsqlTransaction transaction = await connection.BeginTransactionAsync())
            {
                if (await _authRepository.UserExists(newUser.UserName, connection, transaction))
                    throw new UserAlreadyExists();
                
                int UserId = await _authRepository
                    .AddUser(newUser.Adapt<Infrastructure.Models.NewUserRequestModel>(),connection,transaction);

                await transaction.CommitAsync();
            }
        }
    }

    public async Task<LoginResponseModel> Login(LoginUserRequestModel user)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_config.GetConnectionString("Default")))
        {
            await connection.OpenAsync();
            var userFromDB = await _authRepository.GetUserByName(user.UserName, connection);
            
            if (userFromDB == null) throw new IncorrectCredentials();
            
            if (userFromDB.UserPassword.Equals(user.Password))
                return new LoginResponseModel() { Token = _jwtService.GenerateJWT(new Claim("UserId", userFromDB.Id.ToString())) };
            
            throw new IncorrectCredentials();
        }
    }
}