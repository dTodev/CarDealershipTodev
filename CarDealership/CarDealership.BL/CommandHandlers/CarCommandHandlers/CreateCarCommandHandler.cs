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
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreateCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCarCommandHandler> _logger;

        public CreateCarCommandHandler(ICarRepository carRepository, IBrandRepository brandRepository, IMapper mapper, ILogger<CreateCarCommandHandler> logger)
        {
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateCarResponse> Handle(CreateCarCommand carRequest, CancellationToken cancellationToken)
        {
            var auth = await _carRepository.GetCarByModel(carRequest._car.Model);
            var brandExists = await _brandRepository.GetBrandById(carRequest._car.BrandId);

            if (brandExists == null) { 

                _logger.LogError($"Car creation failed due to no brand with id: {carRequest.car.BrandId} !");

                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand with such ID does not exist, create operation not possible!"
                };
            }

            if (auth != null) {

                _logger.LogError($"Car creation failed due to car already existing in DB!");

                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "This car already exist! Try adding a different one or use update operation."
                };
            }

            var car = _mapper.Map<Car>(carRequest._car);

            var result = await _carRepository.CreateCar(car);

            _logger.LogInformation($"Car creation successful!");

            return new CreateCarResponse() 
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Create operation successful, above car was added!",
                Model = result
            };
        }
    }
}
