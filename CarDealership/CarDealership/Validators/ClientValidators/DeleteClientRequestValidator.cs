using CarDealership.Models.Requests.ClientRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class DeleteClientRequestValidator : AbstractValidator<DeleteClientRequest>
    {
        public DeleteClientRequestValidator()
        {
            RuleFor(client => client.Id).NotEmpty().NotEmpty().GreaterThan(0);
        }
    }
}
