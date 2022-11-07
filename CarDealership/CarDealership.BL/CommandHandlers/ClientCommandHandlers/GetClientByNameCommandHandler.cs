using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Responses.ClientResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class GetClientByNameCommandHandler : IRequestHandler<GetClientByNameCommand, GetClientByNameResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<GetClientByNameCommandHandler> _logger;

        public GetClientByNameCommandHandler(IClientRepository clientRepository, ILogger<GetClientByNameCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<GetClientByNameResponse> Handle(GetClientByNameCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetClientByName(request.clientName.Name);

            if (result == null)
            {
                _logger.LogError($"Retrieve client failed due to no client with Name: {request.clientName.Name} found!");

                return new GetClientByNameResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Client with such name does not exist, get operation not possible!"
                };
            }

            _logger.LogInformation($"Retrieve client with Name: {request.clientName.Name} successful!");

            return new GetClientByNameResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved client successfully!",
                Name = result
            };
        }
    }
}
