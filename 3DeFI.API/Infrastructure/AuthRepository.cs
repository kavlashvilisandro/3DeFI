using _3DeFI.API.Domain.Entities;
using _3DeFI.API.Infrastructure.Models;
using Dapper;
using Npgsql;

namespace _3DeFI.API.Infrastructure;

public class AuthRepository : IAuthRepository
{
    public Task<int> AddUser(NewUserRequestModel newUser, NpgsqlConnection connection, NpgsqlTransaction transaction = null)
    {
        string query = "INSERT INTO users(user_name,user_password) values(@UserName,@Password) returning id;";
        return connection.QuerySingleAsync<int>(query, newUser, transaction);
    }

    public async Task<bool> UserExists(string userName, NpgsqlConnection connection, NpgsqlTransaction transaction = null)
    {
        string query = "SELECT true FROM users WHERE user_name = @UserName LIMIT 1;";
        return await connection.QuerySingleOrDefaultAsync<bool>(query,new{ UserName = userName },transaction);
    }

    public Task<UserEntity> GetUserByName(string userName, NpgsqlConnection connection, NpgsqlTransaction transaction = null)
    {
        string query = "SELECT user_name as \"UserName\",user_password as \"UserPassword\", id as \"Id\" FROM users WHERE user_name = @UserName;";
        return connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { UserName = userName }, transaction);
    }
}