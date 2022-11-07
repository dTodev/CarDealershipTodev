using CarDealership.Models.Requests.ClientRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class GetClientByNameRequestValidator : AbstractValidator<GetClientByNameRequest>
    {
        public GetClientByNameRequestValidator()
        {
            RuleFor(client => client.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(35);
        }
    }
}
