using System.Net;
using CarDealership.BL.CommandHandlers.ClientCommandHandlers;
using CarDealership.BL.Services;
using CarDealership.Models.KafkaModels;
using CarDealership.Models.MediatR.PurchaseCommands;
using CarDealership.Models.Responses.ClientResponses;
using CarDealership.Models.Responses.PurchaseResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.PurchaseCommandHandlers
{
    public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, CreatePurchaseResponse>
    {
        private readonly KafkaProducerService<Guid, BasePurchase> _kafkaProducerService;
        private readonly ILogger<CreatePurchaseCommandHandler> _logger;

        public CreatePurchaseCommandHandler(KafkaProducerService<Guid, BasePurchase> kafkaProducerService, ILogger<CreatePurchaseCommandHandler> logger)
        {
            _kafkaProducerService = kafkaProducerService;
            _logger = logger;
        }

        public async Task<CreatePurchaseResponse> Handle(CreatePurchaseCommand purchaseRequest, CancellationToken cancellationToken)
        {
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
                Message = "Purchase published!"
            };

        }
    }
}
