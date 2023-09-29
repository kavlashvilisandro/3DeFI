using _3DeFI.API.Domain.Entities;
using _3DeFI.API.Infrastructure.Models;
using Npgsql;

namespace _3DeFI.API.Infrastructure;

public interface IAuthRepository
{
    Task<int> AddUser(NewUserRequestModel newUser, NpgsqlConnection connection, NpgsqlTransaction transaction = null);
    Task<bool> UserExists(string userName, NpgsqlConnection connection, NpgsqlTransaction transaction = null);
    Task<UserEntity> GetUserByName(string userName, NpgsqlConnection connection, NpgsqlTransaction transaction = null);
}