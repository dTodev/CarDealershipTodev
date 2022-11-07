using CarDealership.Models.Requests.ClientRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class UpdateClientRequestValidator : AbstractValidator<UpdateClientRequest>
    {
        public UpdateClientRequestValidator()
        {
            RuleFor(client => client.Id).NotEmpty().NotEmpty().GreaterThan(0);
            RuleFor(client => client.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(35);
            RuleFor(client => client.Age).NotEmpty().NotNull().GreaterThan(18).LessThan(110);
            RuleFor(client => client.DateOfBirth).NotEmpty().NotNull().GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
        }
    }
}
