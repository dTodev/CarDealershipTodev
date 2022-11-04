using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Requests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreateCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(ICarRepository carRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CreateCarResponse> Handle(CreateCarCommand carRequest, CancellationToken cancellationToken)
        {
            var auth = await _carRepository.GetCarByModel(carRequest._car.Model);
            var brandExists = await _brandRepository.GetBrandById(carRequest._car.BrandId);

            if (brandExists == null)
                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand with such ID does not exist, create operation not possible!"
                };

            if (auth != null)
                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "This car already exist! Try adding a different one or use update operation."
                };

            var car = _mapper.Map<Car>(carRequest._car);
            var result = await _carRepository.CreateCar(car);

            return new CreateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Create operation successful, above car was added!",
                Model = result
            };
        }
    }
}
