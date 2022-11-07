using CarDealership.Models.Requests.BrandRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class DeleteBrandRequestValidator : AbstractValidator<DeleteBrandRequest>
    {
        public DeleteBrandRequestValidator()
        {
            RuleFor(brand => brand.Id).NotEmpty().NotEmpty().GreaterThan(0);
        }
    }
}
