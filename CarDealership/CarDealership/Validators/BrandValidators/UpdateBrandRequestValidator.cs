using CarDealership.Models.Requests.BrandRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class UpdateBrandRequestValidator : AbstractValidator<UpdateBrandRequest>
    {
        public UpdateBrandRequestValidator()
        {
            RuleFor(brand => brand.Id).NotEmpty().NotEmpty().GreaterThan(0);
            RuleFor(brand => brand.BrandName).NotEmpty().NotNull().MinimumLength(1).MaximumLength(25);
        }
    }
}
