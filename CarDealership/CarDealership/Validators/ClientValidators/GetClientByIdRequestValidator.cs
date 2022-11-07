using CarDealership.Models.Requests.ClientRequests;
using FluentValidation;

namespace CarDealership.Validators.CarValidators
{
    public class GetClientByIdRequestValidator : AbstractValidator<GetClientByIdRequest>
    {
        public GetClientByIdRequestValidator()
        {
            RuleFor(client => client.Id).NotEmpty().NotEmpty().GreaterThan(0);
        }
    }
}
