using CarDealership.Models.Requests.ClientRequests;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record CreateClientCommand(CreateClientRequest client) : IRequest<CreateClientResponse>
    {
        public readonly CreateClientRequest _client = client;
    }
}
