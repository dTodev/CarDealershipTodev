using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record DeleteCarCommand(DeleteCarRequest car) : IRequest<DeleteCarResponse>
    {
        public readonly DeleteCarRequest _car = car;
    }
}
