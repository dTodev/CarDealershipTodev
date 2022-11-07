using CarDealership.Models.Requests.PurchaseRequests;
using FluentValidation;

namespace CarDealership.Validators.PurchaseValidators
{
    public class GetAllPurchasesForPeriodRequestValidator : AbstractValidator<GetAllPurchasesForPeriodRequest>
    {
        public GetAllPurchasesForPeriodRequestValidator()
        {
            RuleFor(period => period.From).NotEmpty().NotNull().GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
            RuleFor(period => period.To).NotEmpty().NotNull().GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
        }
    }
}
