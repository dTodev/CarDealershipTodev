using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Responses.ClientResponses;
using CarDealership.Models.Users;
using MediatR;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, DeleteClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<DeleteClientResponse> Handle(DeleteClientCommand clientRequest, CancellationToken cancellationToken)
        {
            var auth = await _clientRepository.GetClientById(clientRequest.client.Id);

            if (auth == null)
                return new DeleteClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Client does not exist, delete operation is not possible!"
                };

            var client = _mapper.Map<Client>(clientRequest.client);
            var result = await _clientRepository.DeleteClient(client.Id);

            return new DeleteClientResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Delete operation successful, the above client was deleted!",
                Id = result
            };
        }
    }
}
