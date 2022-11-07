using System.Threading.Tasks.Dataflow;
using CarDealership.BL.Services;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.Configurations;
using CarDealership.Models.KafkaModels;
using CarDealership.Models.Users;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CarDealership.BL.Dataflow
{
    public class PurchaseDataflow : IHostedService
    {
        private readonly IOptionsMonitor<KafkaSettings> _kafkaSettings;
        private readonly ICarRepository _carRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly KafkaConsumerService<Guid, BasePurchase> _kafkaConsumerService;
        private readonly TransformBlock<BasePurchase, List<Purchase>> _purchaseObjectModelling;
        private readonly TransformBlock<List<Purchase>, List<Purchase>> _enrichPurchaseData;
        private readonly ActionBlock<List<Purchase>> _RecordPurchaseInDB;

        public PurchaseDataflow(IOptionsMonitor<KafkaSettings> kafkaSettings, ICarRepository carRepository, IBrandRepository brandRepository, IClientRepository clientRepository, IPurchaseRepository purchaseRepository)
        {
            _kafkaSettings = kafkaSettings;
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _clientRepository = clientRepository;
            _purchaseRepository = purchaseRepository;

            _purchaseObjectModelling = new TransformBlock<BasePurchase, List<Purchase>>(async request =>
            {
                List<Purchase> tempPurchaseCollection = new List<Purchase>();

                foreach (var carId in request.CarIds)
                {
                    var car = await _carRepository.GetCarById(carId);

                    tempPurchaseCollection.Add(new Purchase()
                    {
                        Id = new Random().Next(0, 100),
                        ClientId = request.ClientId,
                        CarId = carId,
                        BrandId = car.BrandId,
                        Model = car.Model,
                        Price = car.Price,
                    });
                }

                return tempPurchaseCollection;
            });

            _enrichPurchaseData = new TransformBlock<List<Purchase>, List<Purchase>>(async request =>
            {
                foreach (var purchase in request)
                {
                    var brand = await _brandRepository.GetBrandById(purchase.BrandId);
                    var client = await _clientRepository.GetClientById(purchase.ClientId);

                    purchase.Manufacturer = brand.BrandName;
                    purchase.ClientName = client.Name;
                }

                return request;
            });

            _RecordPurchaseInDB = new ActionBlock<List<Purchase>>(async response =>
            {
                foreach (var purchase in response)
                {
                    var result = await _clientRepository.GetClientById(purchase.ClientId);

                    result.TotalPurchases += 1;

                    result.TotalMoneySpent += purchase.Price;

                    await _clientRepository.UpdatePurchaseData(result);

                    await _purchaseRepository.SavePurchase(purchase);
                }
            });

            _purchaseObjectModelling.LinkTo(_enrichPurchaseData);

            _enrichPurchaseData.LinkTo(_RecordPurchaseInDB);

            _kafkaConsumerService = new KafkaConsumerService<Guid, BasePurchase>(_kafkaSettings, PurchaseHandler);
        }

        private void PurchaseHandler(BasePurchase purchase)
        {
            _purchaseObjectModelling.Post(purchase);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => _kafkaConsumerService.Consume());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
