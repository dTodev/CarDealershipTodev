using CarDealership.Models.Requests.BrandRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
    {
        public CreateBrandRequestValidator()
        {
            RuleFor(brand => brand.BrandName).NotEmpty().NotNull().MinimumLength(1).MaximumLength(25);
        }
    }
}
