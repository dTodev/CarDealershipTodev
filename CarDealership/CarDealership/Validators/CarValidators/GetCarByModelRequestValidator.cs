using CarDealership.Models.Requests.CarRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class GetCarByModelRequestValidator : AbstractValidator<GetCarByModelRequest>
    {
        public GetCarByModelRequestValidator()
        {
            RuleFor(car => car.Model).NotEmpty().NotNull().MinimumLength(2).MaximumLength(25);
        }
    }
}
