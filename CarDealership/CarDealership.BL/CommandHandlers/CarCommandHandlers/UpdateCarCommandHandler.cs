using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, UpdateCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCarResponse> Handle(UpdateCarCommand carRequest, CancellationToken cancellationToken)
        {
            var auth = _carRepository.GetCarById(carRequest._car.Id);

            if (auth == null)
                return new UpdateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car does not exist, update operation is not possible!"
                };

            var car = _mapper.Map<Car>(carRequest._car);
            var result = await _carRepository.UpdateCar(car);

            return new UpdateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Updated car successfully!",
                Model = result
            };
        }
    }
}
