using CarDealership.Models.KafkaModels;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class BasePurchaseRequestValidator : AbstractValidator<BasePurchase>
    {
        public BasePurchaseRequestValidator()
        {
            RuleFor(purchase => purchase.ClientId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(purchase => purchase.CarIds).NotEmpty().NotNull();
        }
    }
}
