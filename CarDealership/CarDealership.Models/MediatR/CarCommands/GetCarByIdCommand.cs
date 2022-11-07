using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record GetCarByIdCommand(GetCarByIdRequest carId) : IRequest<GetCarByIdResponse>
    {
        public readonly GetCarByIdRequest _carId = carId;
    }
}
