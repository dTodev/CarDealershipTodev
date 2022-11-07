using CarDealership.Models.Requests.BrandRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class GetBrandByNameRequestValidator : AbstractValidator<GetBrandByNameRequest>
    {
        public GetBrandByNameRequestValidator()
        {
            RuleFor(brand => brand.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(25);
        }
    }
}
