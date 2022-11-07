using CarDealership.BL.Dataflow;
using CarDealership.BL.Services;
using CarDealership.DL.Interfaces;
using CarDealership.DL.Repositories.MsSQLRepos;
using CarDealership.Models.KafkaModels;

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
            services.AddSingleton<IPurchaseRepository, PurchaseRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<KafkaProducerService<Guid, BasePurchase>>();
            services.AddHostedService<PurchaseDataflow>();

            return services;
        }
    }
}
