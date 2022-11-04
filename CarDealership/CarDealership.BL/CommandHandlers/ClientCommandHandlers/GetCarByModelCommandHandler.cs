using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Responses.CarResponses;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class GetClientByNameCommandHandler : IRequestHandler<GetClientByNameCommand, CreateClientResponse>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByNameCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<CreateClientResponse> Handle(GetClientByNameCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetClientByName(request.clientName);

            if (result == null)
                return new CreateClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Client with such name does not exist, get operation not possible!"
                };

            return new CreateClientResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved client successfully!",
                Client = result
            };
        }
    }
}
