using System.Text;
using _3DeFI.API.Application;
using _3DeFI.API.Infrastructure;
using _3DeFI.API.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(a =>
{
    a.AddPolicy("defaultPolicy", c =>
    {
        c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


//Services
builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IDevelopersService, DevelopersService>();


//Repos
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IDevelopersRepository, DevelopersRepository>();



builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

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
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = async (MessageReceivedContext ctx) =>
            {
                ctx.Token = ctx.Request.Headers["Authorization"];
            }
        };
    }));

var app = builder.Build();

app.UseCors("defaultPolicy");
app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseEndpoints((IEndpointRouteBuilder endpoints) =>
{
    endpoints.MapControllers();
});

app.Run();