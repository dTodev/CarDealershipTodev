using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record GetClientByIdCommand(int clientId) : IRequest<CreateClientResponse>
    {
    }
}
