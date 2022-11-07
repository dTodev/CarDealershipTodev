using System.Data.SqlClient;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarDealership.DL.Repositories.MsSQLRepos
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ILogger<CarRepository> _logger;
        private readonly IConfiguration _configuration;

        public PurchaseRepository(ILogger<CarRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Purchase> SavePurchase(Purchase purchase)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Purchases] (ClientId, ClientName, CarId, Manufacturer, Model, Price, DateOfPurchase) VALUES(@ClientId, @ClientName, @CarId, @Manufacturer, @Model, @Price, GETDATE())", purchase);

                    return purchase;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(SavePurchase)}: {e.Message}", e);
            }
            return new Purchase();
        }

        public async Task<IEnumerable<Purchase>> GetAllClientPurchases(int clientId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryAsync<Purchase>("SELECT * FROM Purchases WITH(NOLOCK) WHERE ClientId = @ClientId", new { ClientId = clientId });

                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllClientPurchases)}: {e.Message}", e);
            }
            return Enumerable.Empty<Purchase>();
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesForPeriod(DateTime from, DateTime to)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryAsync<Purchase>("SELECT * FROM Purchases WHERE DateOfPurchase >= @From AND DateOfPurchase <= @To", new { From = from, To = to });

                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllClientPurchases)}: {e.Message}", e);
            }
            return Enumerable.Empty<Purchase>();
        }
    }
}
