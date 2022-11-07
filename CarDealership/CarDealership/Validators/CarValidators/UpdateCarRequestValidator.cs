using CarDealership.Models;
using CarDealership.Models.Requests.CarRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class UpdateCarRequestValidator : AbstractValidator<UpdateCarRequest>
    {
        public UpdateCarRequestValidator()
        {
            RuleFor(car => car.Id).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(car => car.Model).NotEmpty().MinimumLength(2).MaximumLength(25);
            RuleFor(car => car.BrandId).NotNull().GreaterThan(0).LessThanOrEqualTo(10);
            RuleFor(car => car.Quantity).NotNull().GreaterThan(0);
            RuleFor(car => car.Price).NotNull().GreaterThan(0);
        }
    }
}
