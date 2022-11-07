using System.Net;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Requests.CarRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly IMediator _mediator;

        public CarController(ILogger<CarController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(nameof(CreateCar))]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarRequest carRequest)
        {
            _logger.LogInformation($"Car creation with model: {carRequest.Model} requested...");

            var result = await _mediator.Send(new CreateCarCommand(carRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(UpdateCar))]
        public async Task<IActionResult> UpdateCar([FromBody] UpdateCarRequest carRequest)
        {
            _logger.LogInformation($"Car update with model: {carRequest.Model} requested...");

            var result = await _mediator.Send(new UpdateCarCommand(carRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(DeleteCar))]
        public async Task<IActionResult> DeleteCar(DeleteCarRequest carId)
        {
            _logger.LogInformation($"Car removal with ID: {carId.Id} requested...");

            var result = await _mediator.Send(new DeleteCarCommand(carId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetCarById))]
        public async Task<IActionResult> GetCarById([FromQuery] GetCarByIdRequest carId)
        {
            _logger.LogInformation($"Retrieve car with ID: {carId.Id} requested...");

            var result = await _mediator.Send(new GetCarByIdCommand(carId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetCarByModel))]
        public async Task<IActionResult> GetCarByModel([FromQuery] GetCarByModelRequest carModel)
        {
            _logger.LogInformation($"Retrieve car with Model: {carModel.Model} requested...");

            var result = await _mediator.Send(new GetCarByModelCommand(carModel));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetAllCars))]
        public async Task<IActionResult> GetAllCars()
        {
            _logger.LogInformation($"Retrieve all cars list requested...");

            return Ok(await _mediator.Send(new GetAllCarsCommand()));
        }
    }
}