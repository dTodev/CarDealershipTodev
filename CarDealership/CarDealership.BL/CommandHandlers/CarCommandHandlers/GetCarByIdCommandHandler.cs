using System.Net;
using AutoMapper.Internal;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class GetCarByIdCommandHandler : IRequestHandler<GetCarByIdCommand, CreateCarResponse>
    {
        private readonly ICarRepository _carRepository;
        public GetCarByIdCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CreateCarResponse> Handle(GetCarByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepository.GetCarById(request.carId);

            if(result == null)
                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car with such ID does not exist, get operation not possible!"
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
