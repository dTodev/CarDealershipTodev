using CarDealership.Models.Requests.ClientRequests;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record DeleteClientCommand(DeleteClientRequest client) : IRequest<DeleteClientResponse>
    {
        public readonly DeleteClientRequest _client = client;
    }
}
