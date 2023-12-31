using System.Net;
using _3DeFI.API.Application.ServiceAbstractions.Models.ResponseModels;
using _3DeFI.API.Domain.Entities;
using _3DeFI.API.Domain.Exceptions;
using _3DeFI.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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
        //HttpContext context = _contextAccessor.HttpContext;
        //int userId = Convert.ToInt32(context.User.FindFirst("UserId").Value);

        string fileExtension = Path.GetExtension(formFile.FileName).ToLower();
        if (fileExtension != ".html")
            throw new IncorrectFileType();
        using (var stream = new FileStream(_config.GetValue<string>("StaticFilesDirectory") + formFile.FileName, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }
        /*
        using (NpgsqlConnection connection = new NpgsqlConnection(_config.GetConnectionString("Default")))
        {
            await connection.OpenAsync();
            await _developersRepo.UploadFile(userId, fileData, connection);
        }
        */
    }

    public async Task<HttpResponseMessage> GetProjectByName(string fileName)
    {
        string filePath = Path.Combine(_config.GetValue<string>("StaticFilesDirectory"), fileName);
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var fileStream = new FileStream(filePath, FileMode.Open);
        response.Content = new StreamContent(fileStream);
        response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
        response.Content.Headers.ContentDisposition.FileName = fileName;

        return response;

    }
}
public class JavaScriptResult : ContentResult
{
    public JavaScriptResult(string script)
    {
        this.Content = script;
        this.ContentType = "application/javascript";
    }
}