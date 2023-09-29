using _3DeFI.API.Infrastructure;
using Npgsql;

namespace _3DeFI.API.Application;

public class DevelopersService : IDevelopersService
{
    private IHttpContextAccessor _contextAccessor;
    private readonly IDevelopersRepository _developersRepo;
    private readonly IConfiguration _config;
    public DevelopersService(
        IHttpContextAccessor contextAccessor, 
        IDevelopersRepository developersRepo,
        IConfiguration config)
    {
        _contextAccessor = contextAccessor;
        _developersRepo = developersRepo;
        _config = config;
    }
    public async Task UploadProject(IFormFile formFile)
    {
        HttpContext context = _contextAccessor.HttpContext;
        int userId = Convert.ToInt32(context.User.FindFirst("UserId").Value);

        string fileData;
        using (StreamReader reader = new StreamReader(formFile.OpenReadStream()))
        {
            fileData = await reader.ReadToEndAsync();
        }
        using (NpgsqlConnection connection = new NpgsqlConnection(_config.GetConnectionString("Default")))
        {
            await connection.OpenAsync();
            await _developersRepo.UploadFile(userId, fileData, connection);
        }
    }
}