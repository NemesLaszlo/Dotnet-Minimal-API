using Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(Backend.IAssemblyMarker));

var app = builder.Build();

app.UseEndpointDefinitions();

app.UseHttpsRedirection();

app.Run();
