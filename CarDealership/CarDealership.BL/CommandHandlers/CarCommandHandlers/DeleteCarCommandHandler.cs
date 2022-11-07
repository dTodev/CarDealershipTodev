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
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, DeleteCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCarCommandHandler> _logger;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper, ILogger<DeleteCarCommandHandler> logger)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeleteCarResponse> Handle(DeleteCarCommand carRequest, CancellationToken cancellationToken)
        {
            var auth = await _carRepository.GetCarById(carRequest._car.Id);

            if (auth == null)
            {
                _logger.LogError($"Car removal failed due to no car with id: {carRequest.car.Id} !");

                return new DeleteCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car does not exist, delete operation is not possible!"
                };
            }

            var car = _mapper.Map<Car>(carRequest._car);

            var result = await _carRepository.DeleteCar(car.Id);

            _logger.LogInformation($"Car removal successful!");

            return new DeleteCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Delete operation successful, the above car was deleted!",
                Id = result
            };
        }
    }
}
