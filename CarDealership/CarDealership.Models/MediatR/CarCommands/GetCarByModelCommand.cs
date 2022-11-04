using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record GetCarByModelCommand(string carModel) : IRequest<CreateCarResponse>
    {
    }
}
