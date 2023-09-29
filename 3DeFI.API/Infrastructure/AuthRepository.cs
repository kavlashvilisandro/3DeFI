using _3DeFI.API.Infrastructure.Models;
using Npgsql;

namespace _3DeFI.API.Infrastructure;

public class AuthRepository : IAuthRepository
{
    public async Task<int> AddUser(NewUserRequestModel newUser, NpgsqlConnection connection, NpgsqlTransaction transaction = null)
    {
        return 0;
    }
}