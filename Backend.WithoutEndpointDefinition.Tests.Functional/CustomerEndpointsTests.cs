using Backend.WithoutEndpointDefinition.Interfaces;
using Backend.WithoutEndpointDefinition.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Backend.WithoutEndpointDefinition.Tests.Functional;

public class CustomerEndpointsTests
{
    private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();

    [Fact]
    public async Task GetCustomerById_ReturnCustomer_WhenCustomerExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var customer = new Customer(id, "Leslie Nemes");
        _customerRepository.GetById(Arg.Is(id)).Returns(customer);

        using var app = new CustomerEndpointsTestsApp(x =>
        {
            x.AddSingleton(_customerRepository);
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/customers/{id}");
        var responseText = await response.Content.ReadAsStringAsync();
        var customerResult = JsonSerializer.Deserialize<Customer>(responseText);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        customerResult.Should().BeEquivalentTo(customer);
    }

    [Fact]
    public async Task GetCustomerById_ReturnNotFound_WhenCustomerDoesNotExists()
    {
        //Arrange
        _customerRepository.GetById(Arg.Any<Guid>()).Returns((Customer?)null);

        using var app = new CustomerEndpointsTestsApp(x =>
        {
            x.AddSingleton(_customerRepository);
        });

        var guid = Guid.NewGuid();
        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/customers/{guid}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

internal class CustomerEndpointsTestsApp : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection> _serviceOverride;

    public CustomerEndpointsTestsApp(Action<IServiceCollection> serviceOverride)
    {
        _serviceOverride = serviceOverride;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(_serviceOverride);

        return base.CreateHost(builder);
    }
}

