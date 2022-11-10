using System.Net;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Requests.BrandRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IMediator _mediator;

        public BrandController(ILogger<BrandController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(nameof(CreateBrand))]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandRequest brandRequest)
        {
            _logger.LogInformation($"Brand creation with name: {brandRequest.BrandName} requested...");

            var result = await _mediator.Send(new CreateBrandCommand(brandRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(UpdateBrand))]
        public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandRequest brandRequest)
        {
            _logger.LogInformation($"Brand update with name: {brandRequest.BrandName} requested...");

            var result = await _mediator.Send(new UpdateBrandCommand(brandRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(DeleteBrand))]
        public async Task<IActionResult> DeleteBrand([FromBody] DeleteBrandRequest brandId)
        {
            _logger.LogInformation($"Brand removal with ID: {brandId.Id} requested...");

            var result = await _mediator.Send(new DeleteBrandCommand(brandId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return NotFound(result);
        }

        [HttpGet(nameof(GetBrandById))]
        public async Task<IActionResult> GetBrandById([FromQuery] GetBrandByIdRequest brandId)
        {
            _logger.LogInformation($"Retrieve Brand with ID: {brandId.Id} requested...");

            var result = await _mediator.Send(new GetBrandByIdCommand(brandId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetBrandByName))]
        public async Task<IActionResult> GetBrandByName([FromQuery] GetBrandByNameRequest brand)
        {
            _logger.LogInformation($"Retrieve Brand with Name: {brand.Name} requested...");

            var result = await _mediator.Send(new GetBrandByNameCommand(brand));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetAllBrand))]
        public async Task<IActionResult> GetAllBrand()
        {
            _logger.LogInformation($"Retrieve all brands list requested...");

            return Ok(await _mediator.Send(new GetAllBrandsCommand()));
        }
    }
}