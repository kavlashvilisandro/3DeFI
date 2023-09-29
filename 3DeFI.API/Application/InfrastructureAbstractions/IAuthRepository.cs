using _3DeFI.API.Infrastructure.Models;
using Npgsql;

namespace _3DeFI.API.Infrastructure;

public interface IAuthRepository
{
    Task<int> AddUser(NewUserRequestModel newUser, NpgsqlConnection connection, NpgsqlTransaction transaction);
}