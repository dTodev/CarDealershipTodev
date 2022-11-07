using CarDealership.Models.Requests.ClientRequests;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.Models.MediatR.ClientCommands
{
    public record GetClientByIdCommand(GetClientByIdRequest clientId) : IRequest<GetClientByIdResponse>
    {
        public readonly GetClientByIdRequest _clientId = clientId;
    }
}
