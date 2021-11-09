using Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointDefinitions(typeof(Backend.IAssemblyMarker));

var app = builder.Build();

app.UseEndpointDefinitions();

app.UseHttpsRedirection();

app.Run();
