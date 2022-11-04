using System.Data.SqlClient;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarDealership.DL.Repositories.MsSQLRepos
{
    public class CarRepository : ICarRepository
    {
        private readonly ILogger<CarRepository> _logger;
        private readonly IConfiguration _configuration;

        public CarRepository(ILogger<CarRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Car> CreateCar(Car car)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Cars] (BrandId, Model, LastUpdated, Quantity, Price) VALUES(@BrandId, @Model, GETDATE(), @Quantity, @Price)", car);

                    return car;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CreateCar)}: {e.Message}", e);
            }
            return new Car();
        }
        public async Task<Car> UpdateCar(Car car)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE [Cars] SET BrandId = @BrandId, Model = @Model, Quantity = @Quantity, Price = @Price, LastUpdated = GETDATE() WHERE Id = @Id", car);

                    return car;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateCar)}: {e.Message}", e);
            }
            return new Car();
        }

        public async Task<Car> DeleteCar(int carId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var temp = await GetCarById(carId);

                    var result = await conn.ExecuteAsync("DELETE FROM [Cars] WHERE Id = @Id", temp);

                    return temp;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteCar)}: {e.Message}", e);
            }
            return new Car();
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Car>("SELECT * FROM Cars WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllCars)}: {e.Message}", e);
            }
            return Enumerable.Empty<Car>();
        }

        public async Task<Car> GetCarById(int carid)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Car>("SELECT * FROM Cars WITH(NOLOCK) WHERE Id = @Id", new { Id = carid });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetCarById)}: {e.Message}", e);
            }
            return new Car();
        }

        public async Task<Car> GetCarByModel(string carModel)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Car>("SELECT * FROM Cars WITH(NOLOCK) WHERE Model = @Model", new { Model = carModel });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetCarByModel)}: {e.Message}", e);
            }
            return new Car();
        }

        public async Task<Car> GetCarByBrandId(int brandId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Car>("SELECT * FROM Cars WITH(NOLOCK) WHERE BrandId = @BrandId", new { BrandId = brandId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetCarByBrandId)}: {e.Message}", e);
            }
            return new Car();
        }
    }
}
