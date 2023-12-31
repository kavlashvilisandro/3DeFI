using _3DeFI.API.Application.ServiceAbstractions.Models.ResponseModels;

namespace _3DeFI.API.Application;

public interface IDevelopersService
{
    Task UploadProject(IFormFile formFile);
    Task<HttpResponseMessage> GetProjectByName(string name);
}