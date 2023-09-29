using _3DeFI.API.Domain.Entities;
using Dapper;
using Npgsql;

namespace _3DeFI.API.Infrastructure;

public class DevelopersRepository : IDevelopersRepository
{
    public Task UploadFile(int userId, string fileData, NpgsqlConnection connection, NpgsqlTransaction transaction = null)
    {
        string query = "INSERT INTO canvases(owner_id,js_code) values(@OwnerId,@JsCode);";
        return connection.QueryAsync(query, new { OwnerId = userId, JsCode = fileData }, transaction);
    }

    public Task<ProjectEntity> GetById(int projectId, NpgsqlConnection connection, NpgsqlTransaction transaction = null)
    {
        string query = @"SELECT id as Id, owner_id as OwnerId, js_code as JsCode FROM canvases WHERE id = @id;";
        return connection.QuerySingleOrDefaultAsync<ProjectEntity>(query, new { Id = projectId}, transaction);
    }
}