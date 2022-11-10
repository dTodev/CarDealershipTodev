using System.Net;
using CarDealership.Controllers;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Models;
using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarDealership.Tests.CarComponentsTests
{
    public class CarControllerTests
    {
        private IList<Car> _cars = new List<Car>()
        {
            new Car()
                {
                Id = 1,
                Model = "AMG GTR",
                BrandId = 1,
                Quantity = 1,
                Price = 250000,
                LastUpdated = DateTime.Now,
                },
            new Car{
                Id = 2,
                Model = "R8",
                BrandId = 3,
                Quantity = 1,
                Price = 220000,
                LastUpdated = DateTime.Now,
                }
        };

        private Mock<ILogger<CarController>> _loggerCarControllerMock;
        private readonly Mock<IMediator> _mediatorMock;

        public CarControllerTests()
        {
            _loggerCarControllerMock = new Mock<ILogger<CarController>>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Car_GetAllCars_Count_Check()
        {
            //Setup
            var expectedCarsCount = 2;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarsCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_cars);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.GetAllCars();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var cars = okObjectResult.Value as IEnumerable<Car>;
            Assert.NotNull(cars);
            Assert.NotEmpty(cars);
            Assert.Equal(expectedCarsCount, cars.Count());
        }

        [Fact]
        public async Task Car_GetCarById_Ok()
        {
            //Setup
            var carId = new GetCarByIdRequest() { Id = 2 };
            var Car = _cars.First(x => x.Id == carId.Id);
            var expectedCar1 = new GetCarByIdResponse()
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                Message = "Retrieved car successfully!",
                Id = Car
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCarByIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCar1);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.GetCarById(carId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var car = okObjectResult.Value as GetCarByIdResponse;
            Assert.NotNull(car);
            Assert.Equal(expectedCar1.Id, car.Id);
        }

        [Fact]
        public async Task Car_GetCarById_NotFound()
        {
            //Setup
            var carId = new GetCarByIdRequest() { Id = 3 };
            var expectedResponse = new GetCarByIdResponse()
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Message = "Car with such ID does not exist, get operation not possible!"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCarByIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.GetCarById(carId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var response = notFoundObjectResult.Value as GetCarByIdResponse;
            Assert.Equal(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.Equal("Car with such ID does not exist, get operation not possible!", response.Message);
        }

        [Fact]
        public async Task Car_GetCarByModel_Ok()
        {
            //Setup
            var carModel = new GetCarByModelRequest() { Model = "AMG GTR" };
            var Car = _cars.First(x => x.Model == carModel.Model);
            var expectedCar1 = new GetCarByModelResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved car successfully!",
                Model = Car
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCarByModelCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCar1);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.GetCarByModel(carModel);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var car = okObjectResult.Value as GetCarByModelResponse;
            Assert.NotNull(car);
            Assert.Equal(expectedCar1.Model, car.Model);
        }

        [Fact]
        public async Task Car_GetCarByModel_NotFound()
        {
            //Setup
            var carModel = new GetCarByModelRequest() { Model = "911 GTR 3S" };
            var expectedResponse = new GetCarByModelResponse()
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Message = "Car with such model does not exist, get operation not possible!"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCarByModelCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.GetCarByModel(carModel);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var response = notFoundObjectResult.Value as GetCarByModelResponse;
            Assert.Equal(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.Equal("Car with such model does not exist, get operation not possible!", response.Message);
        }

        [Fact]
        public async Task Car_CreateCar_Ok()
        {
            //Setup
            var carRequest = new CreateCarRequest()
            {
                Model = "G63 AMG",
                BrandId = 1,
                Quantity = 1,
                Price = 300000
            };

            var expectedCarCount = 3;

            var Car = new Car()
            {
                Id = 3,
                Model = carRequest.Model,
                BrandId = carRequest.BrandId,
                Quantity = carRequest.Quantity,
                Price = carRequest.Price
            };

            var expectedCar = new CreateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Car creation successful!",
                Model = Car
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateCarCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                _cars.Add(Car);
            })!.ReturnsAsync(expectedCar);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.CreateCar(carRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as CreateCarResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedCarCount, _cars.Count());
            Assert.Equal(resultValue.Model, _cars.FirstOrDefault(x => x.Id == resultValue.Model.Id));
        }

        [Fact]
        public async Task Car_CreateCarWhenAlreadyExists_BadRequest()
        {
            //Setup
            var carRequest = new CreateCarRequest()
            {
                Model = "AMG GTR",
                BrandId = 1,
                Quantity = 1,
                Price = 300000
            };

            var expectedResponse = new CreateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Message = "This car already exist! Try adding a different one or use update operation."
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateCarCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.CreateCar(carRequest);

            //Assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);
            var response = badRequestObjectResult.Value as CreateCarResponse;
            Assert.Equal(HttpStatusCode.BadRequest, response.HttpStatusCode);
            Assert.Equal("This car already exist! Try adding a different one or use update operation.", response.Message);
            Assert.Equal(2, _cars.Count());
        }

        [Fact]
        public async Task Car_CreateCarWhenBrandNotExists_NotFound()
        {
            //Setup
            var carRequest = new CreateCarRequest()
            {
                Model = "AMG GTR",
                BrandId = 1,
                Quantity = 1,
                Price = 300000
            };

            var expectedResponse = new CreateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Message = "Brand with such ID does not exist, create operation not possible!"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateCarCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.CreateCar(carRequest);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var response = notFoundObjectResult.Value as CreateCarResponse;
            Assert.Equal(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.Equal("Brand with such ID does not exist, create operation not possible!", response.Message);
            Assert.Equal(2, _cars.Count());
        }

        [Fact]
        public async Task Car_UpdateCar_Ok()
        {
            //Setup
            var carRequest = new UpdateCarRequest()
            {
                Id = 2,
                Model = "E63S AMG",
                BrandId = 1,
                Quantity = 1,
                Price = 170000
            };

            var Car = new Car()
            {
                Id = carRequest.Id,
                Model = carRequest.Model,
                BrandId = carRequest.BrandId,
                Quantity = carRequest.Quantity,
                Price = carRequest.Price
            };

            var expectedCar = new UpdateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Car update successful!",
                Model = Car
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCarCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                _cars.FirstOrDefault(x => x.Id == carRequest.Id).Model = carRequest.Model;
                _cars.FirstOrDefault(x => x.Id == carRequest.Id).BrandId = carRequest.BrandId;
                _cars.FirstOrDefault(x => x.Id == carRequest.Id).Quantity = carRequest.Quantity;
                _cars.FirstOrDefault(x => x.Id == carRequest.Id).Price = carRequest.Price;

            })!.ReturnsAsync(expectedCar);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.UpdateCar(carRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as UpdateCarResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedCar.Model.Model, _cars.FirstOrDefault(x => x.Id == carRequest.Id).Model);
            Assert.Equal(expectedCar.Model.BrandId, _cars.FirstOrDefault(x => x.Id == carRequest.Id).BrandId);
            Assert.Equal(expectedCar.Model.Quantity, _cars.FirstOrDefault(x => x.Id == carRequest.Id).Quantity);
            Assert.Equal(expectedCar.Model.Price, _cars.FirstOrDefault(x => x.Id == carRequest.Id).Price);
        }

        [Fact]
        public async Task Car_UpdateCar_NotFound()
        {
            //Setup
            var carRequest = new UpdateCarRequest()
            {
                Id = 2,
                Model = "E63S AMG",
                BrandId = 1,
                Quantity = 1,
                Price = 170000
            };

            var expectedResponse = new UpdateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Message = "Car does not exist, update operation is not possible!"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCarCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.UpdateCar(carRequest);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var response = notFoundObjectResult.Value as UpdateCarResponse;
            Assert.Equal(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.Equal("Car does not exist, update operation is not possible!", response.Message);
        }

        [Fact]
        public async Task Car_DeleteCar_Ok()
        {
            //Setup
            var carRequest = new DeleteCarRequest()
            {
                Id = 2
            };

            var expectedCarsCount = 1;

            var deletedCar = _cars.FirstOrDefault(x => x.Id == carRequest.Id);

            var expectedCar = new DeleteCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Car update successful!",
                Id = deletedCar
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteCarCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                _cars.Remove(deletedCar);

            })!.ReturnsAsync(expectedCar);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.DeleteCar(carRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as DeleteCarResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedCarsCount, _cars.Count);
        }

        [Fact]
        public async Task Car_DeleteCar_NotFound()
        {
            //Setup
            var carRequest = new DeleteCarRequest()
            {
                Id = 2
            };

            var expectedResponse = new DeleteCarResponse()
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Message = "Car does not exist, delete operation is not possible!",
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteCarCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

            //Inject
            var controller = new CarController(_loggerCarControllerMock.Object, _mediatorMock.Object);

            //Act
            var result = await controller.DeleteCar(carRequest);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var response = notFoundObjectResult.Value as DeleteCarResponse;
            Assert.Equal(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.Equal("Car does not exist, delete operation is not possible!", response.Message);
        }
    }
}
