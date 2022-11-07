using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.PurchaseCommands;
using CarDealership.Models.Responses.PurchaseResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.PurchaseCommandHandlers
{
    public class GetAllPurchasesOfClientCommandHandler : IRequestHandler<GetAllPurchasesOfClientCommand, GetAllPurchasesOfClientResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ILogger<GetAllPurchasesOfClientCommandHandler> _logger;

        public GetAllPurchasesOfClientCommandHandler(IPurchaseRepository purchaseRepository, ILogger<GetAllPurchasesOfClientCommandHandler> logger)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
        }

        public async Task<GetAllPurchasesOfClientResponse> Handle(GetAllPurchasesOfClientCommand purchaseRequest, CancellationToken cancellationToken)
        {
            var result = await _purchaseRepository.GetAllClientPurchases(purchaseRequest.purchase.Id);

            if (result == null)
            {
                _logger.LogError($"Retrieve of purchases for client with ID: {purchaseRequest.purchase.Id} failed - No purchases found!");

                return new GetAllPurchasesOfClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = $"Retrieve list of purchases failed for client with ID: {purchaseRequest.purchase.Id} - No purchases found!"
                };
            }

            _logger.LogInformation($"Retrieve of purchases successful!");

            return new GetAllPurchasesOfClientResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = $"Retrieve list of purchases for client with ID: {purchaseRequest.purchase.Id} successful!",
                PurchasesList = result
            };

        }
    }
}
