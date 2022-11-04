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
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, DeleteCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<DeleteCarResponse> Handle(DeleteCarCommand carRequest, CancellationToken cancellationToken)
        {
            var auth = await _carRepository.GetCarById(carRequest._car.Id);

            if (auth == null)
                return new DeleteCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car does not exist, delete operation is not possible!"
                };

            var car = _mapper.Map<Car>(carRequest._car);
            var result = await _carRepository.DeleteCar(car.Id);

            return new DeleteCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Delete operation successful, the above car was deleted!",
                Id = result
            };
        }
    }
}
