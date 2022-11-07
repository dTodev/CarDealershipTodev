using CarDealership.Models.Requests.CarRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class GetCarByIdRequestValidator : AbstractValidator<GetCarByIdRequest>
    {
        public GetCarByIdRequestValidator()
        {
            RuleFor(car => car.Id).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
