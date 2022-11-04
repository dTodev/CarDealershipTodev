using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using MediatR;

namespace CarDealership.BL.CommandHandlers.CarCommandHandlers
{
    public class GetAllCarsCommandHandler : IRequestHandler<GetAllCarsCommand, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarsCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(GetAllCarsCommand request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetAllCars();
        }
    }
}
