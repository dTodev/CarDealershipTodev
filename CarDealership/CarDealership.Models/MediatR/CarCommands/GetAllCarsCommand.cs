using CarDealership.Models.Models;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record GetAllCarsCommand : IRequest<IEnumerable<Car>>
    {
    }
}
