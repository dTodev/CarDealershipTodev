using CarDealership.Models.Models;

namespace CarDealership.DL.Interfaces
{
    public interface ICarRepository
    {
        public Task <Car> CreateCar(Car car);
        public Task <Car> UpdateCar(Car car);
        public Task <Car> DeleteCar(int carId);
        public Task <IEnumerable<Car>> GetAllCars();
        public Task <Car> GetCarById(int carid);
        public Task <Car> GetCarByModel(string carModel);
        public Task <Car> GetCarByBrandId(int brandId);
    }
}
