using Npgsql;

namespace _3DeFI.API.Infrastructure;

public interface IDevelopersRepository
{
    Task UploadFile(int userId, string fileData, NpgsqlConnection connection, NpgsqlTransaction transaction = null);
}