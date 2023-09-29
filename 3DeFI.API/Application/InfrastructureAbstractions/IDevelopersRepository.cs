using _3DeFI.API.Domain.Entities;
using Npgsql;

namespace _3DeFI.API.Infrastructure;

public interface IDevelopersRepository
{
    Task UploadFile(int userId, string fileData, NpgsqlConnection connection, NpgsqlTransaction transaction = null);
    Task<ProjectEntity> GetById(int projectId, NpgsqlConnection connection, NpgsqlTransaction transaction = null);

}