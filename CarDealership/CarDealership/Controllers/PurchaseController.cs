using System.Net;
using CarDealership.Models;
using CarDealership.Models.KafkaModels;
using CarDealership.Models.MediatR.PurchaseCommands;
using CarDealership.Models.Requests.PurchaseRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly IMediator _mediator;

        public PurchaseController(ILogger<PurchaseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(nameof(BuyACar))]
        public async Task<IActionResult> BuyACar([FromBody] BasePurchase purchase)
        {
            var result = await _mediator.Send(new CreatePurchaseCommand(purchase));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetAllPurchasesOfClient))]
        public async Task<IActionResult> GetAllPurchasesOfClient([FromQuery] GetAllPurchasesOfClientRequest request)
        {
            var result = await _mediator.Send(new GetAllPurchasesOfClientCommand(request));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetAllPurchasesForPeriod))]
        public async Task<IActionResult> GetAllPurchasesForPeriod([FromQuery] GetAllPurchasesForPeriodRequest period)
        {
            var result = await _mediator.Send(new GetAllPurchasesForPeriodCommand(period));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }
    }
}