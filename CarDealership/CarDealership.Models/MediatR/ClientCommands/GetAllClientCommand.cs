using CarDealership.Models.Users;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record GetAllClientsCommand : IRequest<IEnumerable<Client>>
    {
    }
}
