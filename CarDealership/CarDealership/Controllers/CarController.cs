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
            var result = await _mediator.Send(new CreateCarCommand(carRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(UpdateCar))]
        public async Task<IActionResult> UpdateCar([FromBody] UpdateCarRequest carRequest)
        {
            var result = await _mediator.Send(new UpdateCarCommand(carRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(DeleteCar))]
        public async Task<IActionResult> DeleteCar(DeleteCarRequest carId)
        {
            var result = await _mediator.Send(new DeleteCarCommand(carId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetCarById))]
        public async Task<IActionResult> GetCarById(int carId)
        {
            var result = await _mediator.Send(new GetCarByIdCommand(carId));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetCarByModel))]
        public async Task<IActionResult> GetCarByModel(string model)
        {
            var result = await _mediator.Send(new GetCarByModelCommand(model));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet(nameof(GetAllCars))]
        public async Task<IActionResult> GetAllCars()
        {
            return Ok(await _mediator.Send(new GetAllCarsCommand()));
        }
    }
}