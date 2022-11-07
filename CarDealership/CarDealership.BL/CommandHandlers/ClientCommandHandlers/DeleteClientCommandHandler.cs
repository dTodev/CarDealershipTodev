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
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, DeleteClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteClientCommandHandler> _logger;

        public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper, ILogger<DeleteClientCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeleteClientResponse> Handle(DeleteClientCommand clientRequest, CancellationToken cancellationToken)
        {
            var auth = await _clientRepository.GetClientById(clientRequest.client.Id);

            if (auth == null)
            {
                _logger.LogError($"Client removal failed due to client with ID: {clientRequest.client.Id} not found!");

                return new DeleteClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Client does not exist, delete operation is not possible!"
                };
            }

            var client = _mapper.Map<Client>(clientRequest.client);

            var result = await _clientRepository.DeleteClient(client.Id);

            _logger.LogInformation($"Client removal successful!");

            return new DeleteClientResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Delete operation successful, the above client was deleted!",
                Id = result
            };
        }
    }
}
