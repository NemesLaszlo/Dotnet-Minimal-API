using Backend.WithoutEndpointDefinition.Endpoints;
using Backend.WithoutEndpointDefinition.Interfaces;
using Backend.WithoutEndpointDefinition.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System;
using Xunit;

namespace Backend.WithoutEndpointDefinition.Tests.Unit;

public class CustomerEndpointsTests
{
    private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();

    [Fact]
    public void GetCustomerById_ReturnCustomer_WhenCustomerExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var customer = new Customer(id, "Leslie Nemes");
        _customerRepository.GetById(Arg.Is(id)).Returns(customer);

        //Act
        var result = CustomerEndpoints.GetCustomerById(_customerRepository, id);

        //Assert
        result.GetOkObjectResultValue<Customer>().Should().BeEquivalentTo(customer);
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public void GetCustomerById_ReturnNotFound_WhenCustomerDoesNotExists()
    {
        //Arrange
        _customerRepository.GetById(Arg.Any<Guid>()).Returns((Customer?)null);

        //Act
        var result = CustomerEndpoints.GetCustomerById(_customerRepository, Guid.NewGuid());

        //Assert
        result.GetNotFoundResultStatusCode().Should().Be(404);
    }

}

public static class IResultExtensions
{
    public static T? GetOkObjectResultValue<T>(this IResult result)
    {
        return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("Value",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetOkObjectResultStatusCode(this IResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetNotFoundResultStatusCode(this IResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }
}

