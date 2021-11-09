using Backend.WithoutEndpointDefinition.Interfaces;
using Backend.WithoutEndpointDefinition.Models;
using Backend.WithoutEndpointDefinition.Repositories;

namespace Backend.WithoutEndpointDefinition.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/customers", GetAllCustomers);
        app.MapGet("/customers/{id}", GetCustomerById);
        app.MapPost("/customers", CreateCustomer);
        app.MapPut("/customers/{id}", UpdateCustomer);
        app.MapDelete("/customers/{id}", DeleteCustomerById);
    }

    public static void AddCustomerServices(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
    }

    internal static List<Customer> GetAllCustomers(ICustomerRepository repo)
    {
        return repo.GetAll();
    }

    internal static IResult GetCustomerById(ICustomerRepository repo, Guid id)
    {
        var customer = repo.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal static IResult CreateCustomer(ICustomerRepository repo, Customer customer)
    {
        repo.Create(customer);
        return Results.Created($"/customers/{customer.Id}", customer);
    }

    internal static IResult UpdateCustomer(ICustomerRepository repo, Guid id, Customer updatedCustomer)
    {
        var customer = repo.GetById(id);
        if (customer is null)
        {
            return Results.NotFound();
        }

        repo.Update(updatedCustomer);
        return Results.Ok(updatedCustomer);
    }

    internal static IResult DeleteCustomerById(ICustomerRepository repo, Guid id)
    {
        repo.Delete(id);
        return Results.Ok();
    }
}

