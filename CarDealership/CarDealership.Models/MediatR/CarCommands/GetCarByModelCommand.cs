using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record GetCarByModelCommand(GetCarByModelRequest carModel) : IRequest<GetCarByModelResponse>
    {
        public readonly GetCarByModelRequest _carModel = carModel;
    }
}
