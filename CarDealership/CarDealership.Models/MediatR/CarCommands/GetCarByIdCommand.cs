using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record GetCarByIdCommand(int carId) : IRequest<CreateCarResponse>
    {
    }
}
