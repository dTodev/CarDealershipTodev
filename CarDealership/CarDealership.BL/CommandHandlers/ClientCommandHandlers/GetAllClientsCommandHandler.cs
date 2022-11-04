using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Users;
using MediatR;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class GetAllClientsCommandHandler : IRequestHandler<GetAllClientsCommand, IEnumerable<Client>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientsCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> Handle(GetAllClientsCommand request, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetAllClients();
        }
    }
}
