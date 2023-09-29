using _3DeFI.API.Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();


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