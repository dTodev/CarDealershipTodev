using System.Data.SqlClient;
using CarDealership.DL.Interfaces;
using CarDealership.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarDealership.DL.Repositories.MsSQLRepos
{
    public class ClientRepository : IClientRepository
    {
        private readonly ILogger<CarRepository> _logger;
        private readonly IConfiguration _configuration;

        public ClientRepository(ILogger<CarRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Client> CreateClient(Client client)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Clients] (Name, Age, DateOfBirth, Email, LastUpdated) VALUES(@Name, @Age, @DateOfBirth, @Email, GETDATE())", client);

                    return client;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CreateClient)}: {e.Message}", e);
            }
            return new Client();
        }
        public async Task<Client> UpdateClient(Client client)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE [Clients] SET Name = @Name, Age = @Age, DateOfBirth = @DateOfBirth, LastUpdated = GETDATE() WHERE Id = @Id", client);

                    return client;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateClient)}: {e.Message}", e);
            }
            return new Client();
        }

        public async Task<Client> DeleteClient(int clientId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var temp = await GetClientById(clientId);

                    var result = await conn.ExecuteAsync("DELETE FROM [Clients] WHERE Id = @Id", temp);

                    return temp;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteClient)}: {e.Message}", e);
            }
            return new Client();
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Client>("SELECT * FROM Clients WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllClients)}: {e.Message}", e);
            }
            return Enumerable.Empty<Client>();
        }

        public async Task<Client> GetClientById(int clientId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Clients WITH(NOLOCK) WHERE Id = @Id", new { Id = clientId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetClientById)}: {e.Message}", e);
            }
            return new Client();
        }

        public async Task<Client> GetClientByName(string clientName)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Clients WITH(NOLOCK) WHERE Name = @Name", new { Name = clientName });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetClientByName)}: {e.Message}", e);
            }
            return new Client();
        }

        public async Task<Client> GetClientByEmail(string clientEmail)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Clients WITH(NOLOCK) WHERE Email = @Email", new { Email = clientEmail });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetClientByName)}: {e.Message}", e);
            }
            return new Client();
        }

        public async Task<Client> UpdatePurchaseData(Client client)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE [Clients] SET TotalPurchases = @TotalPurchases, TotalMoneySpent = @TotalMoneySpent, LastPurchaseDate = GETDATE(), LastUpdated = GETDATE() WHERE Id = @Id", client);

                    return client;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdatePurchaseData)}: {e.Message}", e);
            }
            return new Client();
        }
    }
}
