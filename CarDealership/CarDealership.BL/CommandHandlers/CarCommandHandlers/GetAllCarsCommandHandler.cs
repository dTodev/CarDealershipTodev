using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class GetAllCarsCommandHandler : IRequestHandler<GetAllCarsCommand, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<GetAllCarsCommand> _logger;

        public GetAllCarsCommandHandler(ICarRepository carRepository, ILogger<GetAllCarsCommand> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Car>> Handle(GetAllCarsCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepository.GetAllCars();

            if(result == null)
            {
                _logger.LogError("Retrieve all cars list error, no cars in DB!");

                return Enumerable.Empty<Car>();
            }

            _logger.LogInformation("Retrieve all cars list successful!");

            return result;
        }
    }
}
