using System.Net;
using CarDealership.BL.Services;
using CarDealership.DL.Interfaces;
using CarDealership.Models.KafkaModels;
using CarDealership.Models.MediatR.PurchaseCommands;
using CarDealership.Models.Responses.PurchaseResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.PurchaseCommandHandlers
{
    public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, CreatePurchaseResponse>
    {
        private readonly KafkaProducerService<Guid, BasePurchase> _kafkaProducerService;
        private readonly ILogger<CreatePurchaseCommandHandler> _logger;
        private readonly ICarRepository _carRepository;
        private readonly IClientRepository _clientRepository;
        private List<int> _notExistingCars;

        public CreatePurchaseCommandHandler(KafkaProducerService<Guid, BasePurchase> kafkaProducerService, ILogger<CreatePurchaseCommandHandler> logger, ICarRepository carRepository, IClientRepository clientRepository)
        {
            _kafkaProducerService = kafkaProducerService;
            _logger = logger;
            _carRepository = carRepository;
            _clientRepository = clientRepository;
        }

        public async Task<CreatePurchaseResponse> Handle(CreatePurchaseCommand purchaseRequest, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientById(purchaseRequest.purchase.ClientId) == null)
            {
                _logger.LogError($"Purchase publish failed due to client with ID: {purchaseRequest.purchase.ClientId} not existing.");

                return new CreatePurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Message = $"Error, client with ID: {purchaseRequest.purchase.ClientId} do not exist, purchase cannot be made. Try again with existing client."
                };
            }

            _notExistingCars = new List<int>();

            foreach (var id in purchaseRequest._purchase.CarIds)
            {
                //var car = ;
                if (await _carRepository.GetCarById(id) == null)
                {
                    _notExistingCars.Add(id);
                }
            }

            if (_notExistingCars.Any())
            {
                _logger.LogError($"Purchase publish failed due to following cars with ID's: {string.Join(",", _notExistingCars)} not existing.");

                return new CreatePurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Message = $"Error, purchase cannot be made for cars with ID's: {string.Join(",", _notExistingCars)} as they do not exist. Try again with existing cars."
                };
            }

            var tempId = Guid.NewGuid();

            var result = await _kafkaProducerService.ProduceMessage(tempId, purchaseRequest.purchase);

            if (result == null)
            {
                _logger.LogError($"Something went wrong, purchase for client ID: {purchaseRequest.purchase.ClientId} with cars ID: {purchaseRequest.purchase.CarIds} not published!");

                return new CreatePurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Error, purchase not published!"
                };
            }

            _logger.LogInformation($"Purchase published successfully!");

            return new CreatePurchaseResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Purchase sent to kafka!"
            };

        }
    }
}
