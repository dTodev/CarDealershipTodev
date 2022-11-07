using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Responses.ClientResponses;
using CarDealership.Models.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateClientCommandHandler> _logger;

        public CreateClientCommandHandler(IClientRepository clientRepository, IMapper mapper, ILogger<CreateClientCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateClientResponse> Handle(CreateClientCommand clientRequest, CancellationToken cancellationToken)
        {
            var clientExists = await _clientRepository.GetClientByEmail(clientRequest._client.Email);

            if (clientExists != null)
            {
                _logger.LogError($"Client creation failed due to client already existing in DB!");

                return new CreateClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "This client already exists, create operation not possible!"
                };
            }

            var client = _mapper.Map<Client>(clientRequest.client);

            var result = await _clientRepository.CreateClient(client);

            _logger.LogInformation($"Client creation successful!");

            return new CreateClientResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Create operation successful, the above client was added!",
                Client = result
            };
        }
    }
}
