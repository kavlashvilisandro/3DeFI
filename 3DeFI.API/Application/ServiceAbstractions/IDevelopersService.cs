namespace _3DeFI.API.Application;

public interface IDevelopersService
{
    Task UploadProject(IFormFile formFile);
}