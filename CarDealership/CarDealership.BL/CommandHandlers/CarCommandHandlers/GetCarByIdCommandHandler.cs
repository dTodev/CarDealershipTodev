using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Responses.CarResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class GetCarByIdCommandHandler : IRequestHandler<GetCarByIdCommand, GetCarByIdResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<GetCarByIdCommandHandler> _logger;

        public GetCarByIdCommandHandler(ICarRepository carRepository, ILogger<GetCarByIdCommandHandler> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<GetCarByIdResponse> Handle(GetCarByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepository.GetCarById(request.carId.Id);

            if(result == null) {

                _logger.LogError($"Retrieve car failed due to no car with ID: {request.carId} found!");

                return new GetCarByIdResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car with such ID does not exist, get operation not possible!"
                };
            }

            _logger.LogInformation($"Retrieve car with ID: {request.carId} successful!");

            return new GetCarByIdResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved car successfully!",
                Id = result
            };
        }
    }
}
