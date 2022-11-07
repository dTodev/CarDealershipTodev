using CarDealership.Models.Requests.CarRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class DeleteCarRequestValidator : AbstractValidator<DeleteCarRequest>
    {
        public DeleteCarRequestValidator()
        {
            RuleFor(car => car.Id).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
