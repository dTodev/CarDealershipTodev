using CarDealership.Models.Requests.ClientRequests;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record GetClientByNameCommand(GetClientByNameRequest clientName) : IRequest<GetClientByNameResponse>
    {
        public readonly GetClientByNameRequest _clientName = clientName;
    }
}
