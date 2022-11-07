using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class GetClientByIdCommandHandler : IRequestHandler<GetClientByIdCommand, GetClientByIdResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<GetClientByIdCommandHandler> _logger;
        public GetClientByIdCommandHandler(IClientRepository clientRepository, ILogger<GetClientByIdCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<GetClientByIdResponse> Handle(GetClientByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetClientById(request.clientId.Id);

            if (result == null)
            {
                _logger.LogError($"Retrieve client failed due to no client with ID: {request._clientId.Id} found!");

                return new GetClientByIdResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Client with such ID does not exist, get operation not possible!"
                };
            }

            _logger.LogInformation($"Retrieve client with ID: {request._clientId.Id} successful!");

            return new GetClientByIdResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved client successfully!",
                Id = result
            };
        }
    }
}
