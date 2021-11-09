namespace Backend.Interfaces;

public interface IEndpointDefinition
{
    void DefineEndpoints(WebApplication app);
    void DefineServices(IServiceCollection services);
}

