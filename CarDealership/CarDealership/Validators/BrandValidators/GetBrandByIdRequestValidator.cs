using CarDealership.Models.Requests.BrandRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class GetBrandByIdRequestValidator : AbstractValidator<GetBrandByIdRequest>
    {
        public GetBrandByIdRequestValidator()
        {
            RuleFor(brand => brand.Id).NotEmpty().NotEmpty().GreaterThan(0);
        }
    }
}
