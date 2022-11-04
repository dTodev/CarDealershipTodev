using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class GetClientByIdCommandHandler : IRequestHandler<GetClientByIdCommand, CreateClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        public GetClientByIdCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<CreateClientResponse> Handle(GetClientByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetClientById(request.clientId);

            if(result == null)
                return new CreateClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Client with such ID does not exist, get operation not possible!"
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
