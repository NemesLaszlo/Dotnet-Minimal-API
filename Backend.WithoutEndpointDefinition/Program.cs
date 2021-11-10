using Backend.WithoutEndpointDefinition.Endpoints;
using Backend.WithoutEndpointDefinition.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// Service Config
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapCustomerEndpoints();

app.UseHttpsRedirection();
app.Run();
