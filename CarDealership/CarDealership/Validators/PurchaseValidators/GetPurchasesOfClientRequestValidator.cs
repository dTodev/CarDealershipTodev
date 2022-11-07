using CarDealership.Models.Requests.PurchaseRequests;
using FluentValidation;

namespace CarDealership.Validators.PurchaseValidators
{
    public class GetPurchasesOfClientRequestValidator : AbstractValidator<GetAllPurchasesOfClientRequest>
    {
        public GetPurchasesOfClientRequestValidator()
        {
            RuleFor(client => client.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
