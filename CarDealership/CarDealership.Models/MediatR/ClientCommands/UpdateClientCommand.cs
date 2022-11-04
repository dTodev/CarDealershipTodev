using CarDealership.Models.Requests.ClientRequests;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record UpdateClientCommand(UpdateClientRequest client) : IRequest<UpdateClientResponse>
    {
        public readonly UpdateClientRequest _client = client;
    }
}
