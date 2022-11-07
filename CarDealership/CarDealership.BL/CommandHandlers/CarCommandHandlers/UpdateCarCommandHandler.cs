using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Responses.CarResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, UpdateCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCarCommandHandler> _logger;

        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper, ILogger<UpdateCarCommandHandler> logger)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateCarResponse> Handle(UpdateCarCommand carRequest, CancellationToken cancellationToken)
        {
            var auth = _carRepository.GetCarById(carRequest._car.Id);

            if (auth == null)
            {
                _logger.LogError($"Car update failed due to no car with id: {carRequest.car.Id} !");

                return new UpdateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car does not exist, update operation is not possible!"
                };
            }

            var car = _mapper.Map<Car>(carRequest._car);

            var result = await _carRepository.UpdateCar(car);

            _logger.LogInformation($"Car update successful!");

            return new UpdateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Updated car successfully!",
                Model = result
            };
        }
    }
}
