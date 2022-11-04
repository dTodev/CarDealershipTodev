using CarDealership.BL.CommandHandlers.BrandCommandHandlers;
using CarDealership.BL.CommandHandlers.CarCommandHandlers;
using CarDealership.DL.Interfaces;
using CarDealership.DL.Repositories.MsSQLRepos;

namespace CarDealership.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICarRepository, CarRepository>();
            services.AddSingleton<IBrandRepository, BrandRepository>();
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            

            return services;
        }
    }
}
