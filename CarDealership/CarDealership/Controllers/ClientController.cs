using System.Net;
using CarDealership.Models.MediatR.ClientCommands;
using CarDealership.Models.Requests.ClientRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(ILogger<ClientController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(nameof(CreateClient))]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientRequest clientRequest)
        {
            var result = await _mediator.Send(new CreateClientCommand(clientRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(UpdateClient))]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientRequest clientRequest)
        {
            var result = await _mediator.Send(new UpdateClientCommand(clientRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(DeleteClient))]
        public async Task<IActionResult> DeleteClient(DeleteClientRequest clientId)
        {
            var result = await _mediator.Send(new DeleteClientCommand(clientId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetClientById))]
        public async Task<IActionResult> GetClientById([FromQuery] GetClientByIdRequest clientId)
        {
            var result = await _mediator.Send(new GetClientByIdCommand(clientId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetClientByName))]
        public async Task<IActionResult> GetClientByName([FromQuery] GetClientByNameRequest clientName)
        {
            var result = await _mediator.Send(new GetClientByNameCommand(clientName));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetAllClients))]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _mediator.Send(new GetAllClientsCommand()));
        }
    }
}