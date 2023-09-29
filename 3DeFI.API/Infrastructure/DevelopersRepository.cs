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
}