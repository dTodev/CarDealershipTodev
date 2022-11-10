using AutoMapper;
using CarDealership.AutoMapper;
using CarDealership.BL.CommandHandlers.CarCommandHandlers;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.CarCommands;
using CarDealership.Models.Models;
using CarDealership.Models.Requests.CarRequests;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarDealership.Tests.CarComponentsTests
{
    public class CarMediatorTests
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

        private readonly IMapper _mapper;
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly Mock<IBrandRepository> _brandRepositoryMock;

        public CarMediatorTests()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapperConfig.CreateMapper();
            _carRepositoryMock = new Mock<ICarRepository>();
            _brandRepositoryMock = new Mock<IBrandRepository>();
        }

        [Fact]
        public async Task Car_GetAllCars_Count_Check()
        {
            //Setup
            var expectedCarsCount = 2;

            var command = new GetAllCarsCommand();

            var _loggerMock = new Mock<ILogger<GetAllCarsCommandHandler>>();

            _carRepositoryMock.Setup(x => x.GetAllCars()).ReturnsAsync(_cars);

            //Inject
            var commandHandler = new GetAllCarsCommandHandler(_carRepositoryMock.Object, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedCarsCount, result.Count());
        }

        [Fact]
        public async Task Car_GetCarById_Ok()
        {
            //Setup
            var request = new GetCarByIdRequest() { Id = 1 };

            var command = new GetCarByIdCommand(request) { carId = request };

            var car = _cars.FirstOrDefault(x => x.Id == request.Id);

            _carRepositoryMock.Setup(x => x.GetCarById(request.Id)).ReturnsAsync(car);

            var _loggerMock = new Mock<ILogger<GetCarByIdCommandHandler>>();

            //Inject
            var commandHandler = new GetCarByIdCommandHandler(_carRepositoryMock.Object, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.HttpStatusCode);

            var actualCar = result.Id;
            Assert.Equal(car, actualCar);
        }

        [Fact]
        public async Task Car_GetCarById_NotFound()
        {
            //Setup
            var request = new GetCarByIdRequest() { Id = 3 };

            var command = new GetCarByIdCommand(request) { carId = request };

            var car = _cars.FirstOrDefault(x => x.Id == request.Id);

            _carRepositoryMock.Setup(x => x.GetCarById(request.Id)).ReturnsAsync(car);

            var _loggerMock = new Mock<ILogger<GetCarByIdCommandHandler>>();

            //Inject
            var commandHandler = new GetCarByIdCommandHandler(_carRepositoryMock.Object, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.HttpStatusCode);
        }

        [Fact]
        public async Task Car_GetCarByModel_Ok()
        {
            //Setup
            var request = new GetCarByModelRequest() { Model = "AMG GTR" };

            var command = new GetCarByModelCommand(request) { carModel = request };

            var car = _cars.FirstOrDefault(x => x.Model == request.Model);

            _carRepositoryMock.Setup(x => x.GetCarByModel(request.Model)).ReturnsAsync(car);

            var _loggerMock = new Mock<ILogger<GetCarByModelCommandHandler>>();

            //Inject
            var commandHandler = new GetCarByModelCommandHandler(_carRepositoryMock.Object, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.HttpStatusCode);

            var actualCar = result.Model;
            Assert.Equal(car, actualCar);
        }

        [Fact]
        public async Task Car_GetCarByModel_NotFound()
        {
            //Setup
            var request = new GetCarByModelRequest() { Model = "Continental GT" };

            var command = new GetCarByModelCommand(request) { carModel = request };

            var car = _cars.FirstOrDefault(x => x.Model == request.Model);

            _carRepositoryMock.Setup(x => x.GetCarByModel(request.Model)).ReturnsAsync(car);

            var _loggerMock = new Mock<ILogger<GetCarByModelCommandHandler>>();

            //Inject
            var commandHandler = new GetCarByModelCommandHandler(_carRepositoryMock.Object, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.HttpStatusCode);
        }

        [Fact]
        public async Task Car_CreateCar_Ok()
        {
            //Setup
            var expectedCarsCount = 3;

            var request = new CreateCarRequest()
            {
                Model = "E63S AMG",
                BrandId = 3,
                Quantity = 2,
                Price = 123456
            };

            var car = new Car()
            {
                Id = 3,
                Model = request.Model,
                BrandId = request.BrandId,
                Quantity = request.Quantity,
                Price = request.Price
            };

            var brand = new Brand()
            {
                Id = 1,
                BrandName = "Test",
            };

            var command = new CreateCarCommand(request) { car = request };

            _carRepositoryMock.Setup(x => x.GetCarByModel(car.Model)).ReturnsAsync((Car)(null));
            _brandRepositoryMock.Setup(x => x.GetBrandById(car.BrandId)).ReturnsAsync(brand);

            _carRepositoryMock.Setup(x => x.CreateCar(It.IsAny<Car>())).Callback(() =>
            {
                _cars.Add(car);
            }).ReturnsAsync(car);

            var _loggerMock = new Mock<ILogger<CreateCarCommandHandler>>();

            //Inject
            var commandHandler = new CreateCarCommandHandler(_carRepositoryMock.Object, _brandRepositoryMock.Object, _mapper, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.HttpStatusCode);

            var actualCar = result.Model;
            Assert.Equal(car, _cars.FirstOrDefault(x => x.Id == actualCar.Id));
            Assert.Equal(expectedCarsCount, _cars.Count());
        }

        [Fact]
        public async Task Car_CreateCarWhenExists_BadRequest()
        {
            //Setup
            var request = new CreateCarRequest()
            {
                Model = "AMG GTR",
                BrandId = 1,
                Quantity = 1,
                Price = 250000
            };

            var car = _cars.FirstOrDefault(x => x.Model == request.Model);

            var brand = new Brand()
            {
                Id = 1,
                BrandName = "Test",
            };

            var command = new CreateCarCommand(request) { car = request };

            _carRepositoryMock.Setup(x => x.GetCarByModel(request.Model)).ReturnsAsync(car);
            _brandRepositoryMock.Setup(x => x.GetBrandById(request.BrandId)).ReturnsAsync(brand);

            var _loggerMock = new Mock<ILogger<CreateCarCommandHandler>>();

            //Inject
            var commandHandler = new CreateCarCommandHandler(_carRepositoryMock.Object, _brandRepositoryMock.Object, _mapper, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.HttpStatusCode);
            Assert.Equal("This car already exist! Try adding a different one or use update operation.", result.Message);
            Assert.Equal(2, _cars.Count());
        }

        [Fact]
        public async Task Car_CreateCarWhenBrandNotExists_NotFound()
        {
            //Setup
            var request = new CreateCarRequest()
            {
                Model = "911 GT3 RS",
                BrandId = 4,
                Quantity = 1,
                Price = 250000
            };

            var car = _cars.FirstOrDefault(x => x.Model == request.Model);

            var command = new CreateCarCommand(request) { car = request };

            _carRepositoryMock.Setup(x => x.GetCarByModel(request.Model)).ReturnsAsync(car);
            _brandRepositoryMock.Setup(x => x.GetBrandById(request.BrandId)).ReturnsAsync((Brand)(null));

            var _loggerMock = new Mock<ILogger<CreateCarCommandHandler>>();

            //Inject
            var commandHandler = new CreateCarCommandHandler(_carRepositoryMock.Object, _brandRepositoryMock.Object, _mapper, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.HttpStatusCode);
            Assert.Equal("Brand with such ID does not exist, create operation not possible!", result.Message);
            Assert.Equal(2, _cars.Count());
        }

        [Fact]
        public async Task Car_UpdateCar_Ok()
        {
            //Setup
            var request = new UpdateCarRequest()
            {
                Id = 2,
                Model = "E63S AMG",
                BrandId = 3,
                Quantity = 2,
                Price = 123456
            };

            var car = new Car()
            {
                Id = request.Id,
                Model = request.Model,
                BrandId = request.BrandId,
                Quantity = request.Quantity,
                Price = request.Price
            };

            var command = new UpdateCarCommand(request) { car = request };

            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<int>())).ReturnsAsync(car);

            _carRepositoryMock.Setup(x => x.UpdateCar(It.IsAny<Car>())).Callback(() =>
            {
                _cars.FirstOrDefault(x => x.Id == request.Id).Model = request.Model;
                _cars.FirstOrDefault(x => x.Id == request.Id).BrandId = request.BrandId;
                _cars.FirstOrDefault(x => x.Id == request.Id).Quantity = request.Quantity;
                _cars.FirstOrDefault(x => x.Id == request.Id).Price = request.Price;
            }).ReturnsAsync(car);

            var _loggerMock = new Mock<ILogger<UpdateCarCommandHandler>>();

            //Inject
            var commandHandler = new UpdateCarCommandHandler(_carRepositoryMock.Object, _mapper, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.HttpStatusCode);
            Assert.Equal(car.Model, _cars.FirstOrDefault(x => x.Id == request.Id).Model);
            Assert.Equal(car.BrandId, _cars.FirstOrDefault(x => x.Id == request.Id).BrandId);
            Assert.Equal(car.Quantity, _cars.FirstOrDefault(x => x.Id == request.Id).Quantity);
            Assert.Equal(car.Price, _cars.FirstOrDefault(x => x.Id == request.Id).Price);
        }

        [Fact]
        public async Task Car_UpdateCarWhenNotExists_NotFound()
        {
            //Setup
            var request = new UpdateCarRequest()
            {
                Id = 3,
                Model = "E63S AMG",
                BrandId = 3,
                Quantity = 2,
                Price = 123456
            };

            var car = _cars.FirstOrDefault(x => x.Id == request.Id);

            var command = new UpdateCarCommand(request) { car = request };

            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<int>())).ReturnsAsync(_cars.FirstOrDefault(x => x.Id == request.Id));

            var _loggerMock = new Mock<ILogger<UpdateCarCommandHandler>>();

            //Inject
            var commandHandler = new UpdateCarCommandHandler(_carRepositoryMock.Object, _mapper, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.HttpStatusCode);
            Assert.Equal("Car does not exist, update operation is not possible!", result.Message);
        }

        [Fact]
        public async Task Car_DeleteCar_Ok()
        {
            //Setup
            var request = new DeleteCarRequest(){ Id = 2 };

            var expectedCarsCount = 1;

            var deletedCar = _cars.FirstOrDefault(x => x.Id == request.Id);

            var command = new DeleteCarCommand(request) { car = request };

            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<int>())).ReturnsAsync(deletedCar);

            _carRepositoryMock.Setup(x => x.DeleteCar(request.Id)).Callback(() =>
            {
                _cars.Remove(_cars.FirstOrDefault(x => x.Id == request.Id));
            }).ReturnsAsync(deletedCar);

            var _loggerMock = new Mock<ILogger<DeleteCarCommandHandler>>();

            //Inject
            var commandHandler = new DeleteCarCommandHandler(_carRepositoryMock.Object, _mapper, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.HttpStatusCode);
            Assert.Equal(expectedCarsCount, _cars.Count);
        }

        [Fact]
        public async Task Car_DeleteCarWhenNotExists_NotFound()
        {
            //Setup
            var request = new DeleteCarRequest() { Id = 3 };

            var command = new DeleteCarCommand(request) { car = request };

            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<int>())).ReturnsAsync(_cars.FirstOrDefault(x => x.Id == request.Id));

            var _loggerMock = new Mock<ILogger<DeleteCarCommandHandler>>();

            //Inject
            var commandHandler = new DeleteCarCommandHandler(_carRepositoryMock.Object, _mapper, _loggerMock.Object);

            //Act
            var result = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.HttpStatusCode);
            Assert.Equal("Car does not exist, delete operation is not possible!", result.Message);
        }
    }
}

