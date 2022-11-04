using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;
using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;

namespace CarDealership.BL.Interfaces
{
    public interface ICarService
    {
        public Task <CreateCarResponse> CreateCar(CreateCarRequest car);
        public Task <UpdateCarResponse> UpdateCar(UpdateCarRequest car);
        public Task <Car> DeleteCar(int carId);
        public Task <Car> GetCarById(int carId);
        public Task <Car> GetCarByModel(string carModel);
    }
}
