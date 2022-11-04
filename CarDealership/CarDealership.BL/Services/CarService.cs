using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDealership.BL.Interfaces;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;

namespace CarDealership.BL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CreateCarResponse> CreateCar(CreateCarRequest carRequest)
        {
            var auth = await _carRepository.GetCarByModel(carRequest.Model);
            var brandExists = await _brandRepository.GetBrandById(carRequest.BrandId);

            if (brandExists == null)
                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand with such ID does not exist, create operation not possible!"
                };

            if (auth != null)
                return new CreateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "This car already exist! Try adding a different one or use update operation."
                };

            var car = _mapper.Map<Car>(carRequest);
            var result = await _carRepository.CreateCar(car);

            return new CreateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Model = result
            };
        }

        public async Task<UpdateCarResponse> UpdateCar(UpdateCarRequest carRequest)
        {
            var auth = _carRepository.GetCarByModel(carRequest.Model);

            if (auth == null)
                return new UpdateCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car does not exist, update operation is not possible!"
                };

            var car = _mapper.Map<Car>(carRequest);
            var result = await _carRepository.UpdateCar(car);

            return new UpdateCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Model = result
            };
        }

        public async Task<Car> DeleteCar(int carId)
        {
            return await _carRepository.DeleteCar(carId);
        }

        public async Task<Car> GetCarById(int carId)
        {
            return await _carRepository.GetCarById(carId);
        }

        public async Task<Car> GetCarByModel(string carModel)
        {
            return await _carRepository.GetCarByModel(carModel);
        }
    }
}
