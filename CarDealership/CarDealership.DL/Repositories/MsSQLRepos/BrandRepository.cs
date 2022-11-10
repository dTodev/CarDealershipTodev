using System.Data.SqlClient;
using CarDealership.DL.Interfaces;
using CarDealership.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarDealership.DL.Repositories.MsSQLRepos
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ILogger<CarRepository> _logger;
        private readonly IConfiguration _configuration;

        public BrandRepository(ILogger<CarRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Brand> CreateBrand(Brand brand)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Brands] (BrandName, LastUpdated) VALUES(@BrandName, GETDATE())", brand);

                    return brand;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CreateBrand)}: {e.Message}", e);
            }
            return new Brand();
        }
        public async Task<Brand> UpdateBrand(Brand brand)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE [Brands] SET BrandName = @BrandName WHERE Id = @Id", brand);

                    return brand;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateBrand)}: {e.Message}", e);
            }
            return new Brand();
        }

        public async Task<Brand> DeleteBrand(int brandId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var temp = await GetBrandById(brandId);

                    var result = await conn.ExecuteAsync("DELETE FROM [Brands] WHERE Id = @Id", temp);

                    return temp;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteBrand)}: {e.Message}", e);
            }
            return new Brand();
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Brand>("SELECT * FROM Brands WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllBrands)}: {e.Message}", e);
            }
            return Enumerable.Empty<Brand>();
        }

        public async Task<Brand> GetBrandById(int brandid)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Brand>("SELECT * FROM Brands WITH(NOLOCK) WHERE Id = @Id", new { Id = brandid });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetBrandById)}: {e.Message}", e);
            }
            return new Brand();
        }

        public async Task<Brand> GetBrandByName(string brandName)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Brand>("SELECT * FROM Brands WITH(NOLOCK) WHERE BrandName = @BrandName", new { BrandName = brandName });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetBrandByName)}: {e.Message}", e);
            }
            return new Brand();
        }
    }
}
