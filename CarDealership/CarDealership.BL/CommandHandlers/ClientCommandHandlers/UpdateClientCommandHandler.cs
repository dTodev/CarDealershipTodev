using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Responses.CarResponses;
using CarDealership.Models.Responses.ClientResponses;
using CarDealership.Models.Users;
using MediatR;

namespace CarDealership.BL.CommandHandlers.ClientCommandHandlers
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, UpdateClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<UpdateClientResponse> Handle(UpdateClientCommand clientRequest, CancellationToken cancellationToken)
        {
            var auth = await _clientRepository.GetClientById(clientRequest._client.Id);

            if (auth == null)
                return new UpdateClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Client with such ID does not exist, update operation is not possible!"
                };

            var client = _mapper.Map<Client>(clientRequest._client);
            var result = await _clientRepository.UpdateClient(client);

            return new UpdateClientResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Updated client successfully!",
                Name = result
            };
        }
    }
}
