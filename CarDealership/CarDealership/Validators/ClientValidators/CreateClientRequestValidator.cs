using CarDealership.Models.Requests.ClientRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator()
        {
            RuleFor(client => client.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(35);
            RuleFor(client => client.Age).NotEmpty().NotNull().GreaterThan(18).LessThan(110);
            RuleFor(client => client.Email).NotEmpty().NotEmpty().MinimumLength(5).MaximumLength(60);
            RuleFor(client => client.DateOfBirth).NotEmpty().NotNull().GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
        }
    }
}
