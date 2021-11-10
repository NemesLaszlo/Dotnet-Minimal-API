using Backend.WithoutEndpointDefinition.Models;
using FluentValidation;

namespace Backend.WithoutEndpointDefinition.Validation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MinimumLength(2);
    }
}

