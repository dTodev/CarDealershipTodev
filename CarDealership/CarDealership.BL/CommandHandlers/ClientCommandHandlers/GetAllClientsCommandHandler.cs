using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class GetAllClientsCommandHandler : IRequestHandler<GetAllClientsCommand, IEnumerable<Client>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<GetAllClientsCommandHandler> _logger;

        public GetAllClientsCommandHandler(IClientRepository clientRepository, ILogger<GetAllClientsCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Client>> Handle(GetAllClientsCommand request, CancellationToken cancellationToken)
        {
            var result =  await _clientRepository.GetAllClients();

            if (result == null)
            {
                _logger.LogError("Retrieve all clients list error, no clients in DB!");

                return Enumerable.Empty<Client>();
            }

            _logger.LogInformation("Retrieve all clients list successful!");

            return result;
        }
    }
}
