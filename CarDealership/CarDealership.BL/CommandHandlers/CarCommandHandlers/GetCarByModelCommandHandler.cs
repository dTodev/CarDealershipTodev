using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class GetCarByModelCommandHandler : IRequestHandler<GetCarByModelCommand, CreateCarResponse>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByModelCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CreateCarResponse> Handle(GetCarByModelCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepository.GetCarByModel(request.carModel);
            if (result == null)
                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car with such model does not exist, get operation not possible!"
                };

            return new CreateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved car successfully!",
                Model = result
            };
        }
    }
}
