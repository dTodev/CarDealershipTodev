using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Responses.CarResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class GetCarByModelCommandHandler : IRequestHandler<GetCarByModelCommand, GetCarByModelResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<GetCarByModelCommandHandler> _logger;

        public GetCarByModelCommandHandler(ICarRepository carRepository, ILogger<GetCarByModelCommandHandler> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<GetCarByModelResponse> Handle(GetCarByModelCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepository.GetCarByModel(request._carModel.Model);

            if (result == null)
            {
                _logger.LogError($"Retrieve car failed due to no car with Model: {request.carModel} found!");

                return new GetCarByModelResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car with such model does not exist, get operation not possible!"
                };
            }

            _logger.LogInformation($"Retrieve car with Model: {request.carModel} successful!");

            return new GetCarByModelResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved car successfully!",
                Model = result
            };
        }
    }
}
