using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarDealership.HealthChecks
{
    public class SQLHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public SQLHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);
                }
                catch (SqlException exception)
                {
                    return HealthCheckResult.Unhealthy(exception.Message);
                }
            }
            return HealthCheckResult.Healthy("SQL Connection : HEALTHY");
        }
    }
}
