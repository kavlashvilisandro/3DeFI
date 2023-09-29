using System.Text;
using _3DeFI.API.Application;
using _3DeFI.API.Presentation;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//Services
builder.Services.AddSingleton<IJWTService, JWTService>();

//Repos
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication()
    .AddJwtBearer("UserAuth", (options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:PrivateKey"))),
            ValidateAudience = false
        };
    }));

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.UseRouting();


app.UseSwagger();
app.UseSwaggerUI();

app.UseEndpoints((IEndpointRouteBuilder endpoints) =>
{
    endpoints.MapControllers();
});

app.Run();