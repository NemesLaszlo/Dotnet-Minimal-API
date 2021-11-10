using Backend.WithoutEndpointDefinition.Interfaces;
using Backend.WithoutEndpointDefinition.Models;
using Backend.WithoutEndpointDefinition.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WithoutEndpointDefinition.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/customers", GetAllCustomers).AllowAnonymous();
        app.MapGet("/customers/{id}", GetCustomerById).AllowAnonymous();
        app.MapPost("/customers", CreateCustomer).AllowAnonymous();
        app.MapPut("/customers/{id}", UpdateCustomer);
        app.MapDelete("/customers/{id}", DeleteCustomerById);
    }

    public static void AddCustomerServices(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Customer>());
    }

    [ProducesResponseType(200, Type = (typeof(Customer)))]
    internal static IResult GetAllCustomers(ICustomerRepository repo)
    {
        return Results.Ok(repo.GetAll());
    }

    internal static IResult GetCustomerById(ICustomerRepository repo, Guid id)
    {
        var customer = repo.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal static IResult CreateCustomer(ICustomerRepository repo, IValidator<Customer> validator, Customer customer)
    {
        var validationResult = validator.Validate(customer);
        if (!validationResult.IsValid)
        {
            var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
            return Results.BadRequest(errors);
        }

        repo.Create(customer);
        return Results.Created($"/customers/{customer.Id}", customer);
    }

    internal static IResult UpdateCustomer(ICustomerRepository repo, IValidator<Customer> validator, Guid id, Customer updatedCustomer)
    {
        var customer = repo.GetById(id);
        if (customer is null)
        {
            return Results.NotFound();
        }

        var validationResult = validator.Validate(updatedCustomer);
        if (!validationResult.IsValid)
        {
            var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
            return Results.BadRequest(errors);
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

