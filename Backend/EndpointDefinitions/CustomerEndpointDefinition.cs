using Backend.Interfaces;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.EndpointDefinitions;

public class CustomerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/customers", GetAllCustomers);
        app.MapGet("/customers/{id}", GetCustomerById);
        app.MapPost("/customers", CreateCustomer);
        app.MapPut("/customers/{id}", UpdateCustomer);
        app.MapDelete("/customers/{id}", DeleteCustomerById);
    }

    internal List<Customer> GetAllCustomers(ICustomerRepository repo)
    {
        return repo.GetAll();
    }

    internal IResult GetCustomerById(ICustomerRepository repo, Guid id)
    {
        var customer = repo.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal IResult CreateCustomer(ICustomerRepository repo, Customer customer)
    {
        repo.Create(customer);
        return Results.Created($"/customers/{customer.Id}", customer);
    }

    internal IResult UpdateCustomer(ICustomerRepository repo, Guid id, Customer updatedCustomer)
    {
        var customer = repo.GetById(id);
        if (customer is null)
        {
            return Results.NotFound();
        }

        repo.Update(updatedCustomer);

        return Results.Ok(updatedCustomer);
    }

    internal IResult DeleteCustomerById(ICustomerRepository repo, Guid id)
    {
        repo.Delete(id);
        return Results.Ok();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
    }
}

