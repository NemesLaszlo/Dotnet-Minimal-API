using System.Text.Json.Serialization;

namespace Backend.WithoutEndpointDefinition.Models;

public class Customer
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [JsonPropertyName("fullName")]
    public string FullName { get; init; }

    //Used for Json
    public Customer() { }

    public Customer(string fullName)
    {
        FullName = fullName;
    }

    public Customer(Guid id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }
}

