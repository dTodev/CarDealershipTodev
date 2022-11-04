using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record GetClientByNameCommand(string clientName) : IRequest<CreateClientResponse>
    {
    }
}
