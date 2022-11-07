using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.PurchaseCommands;
using CarDealership.Models.Responses.PurchaseResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.PurchaseCommandHandlers
{
    public class GetAllPurchasesForPeriodCommandHandler : IRequestHandler<GetAllPurchasesForPeriodCommand, GetAllPurchasesForPeriodResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ILogger<GetAllPurchasesForPeriodCommandHandler> _logger;

        public GetAllPurchasesForPeriodCommandHandler(IPurchaseRepository purchaseRepository, ILogger<GetAllPurchasesForPeriodCommandHandler> logger)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
        }

        public async Task<GetAllPurchasesForPeriodResponse> Handle(GetAllPurchasesForPeriodCommand request, CancellationToken cancellationToken)
        {
            var result = await _purchaseRepository.GetAllPurchasesForPeriod(request.period.From, request._period.To);

            if (!result.Any())
            {
                _logger.LogError($"Retrieve of purchases in period: {request.period.From} - {request.period.To} failed - No purchases found!");

                return new GetAllPurchasesForPeriodResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = $"Retrieve list of purchases failed for period: From:{request.period.From} - To:{request.period.To} - No purchases found!"
                };
            }

            _logger.LogInformation($"Retrieve of purchases successful!");

            return new GetAllPurchasesForPeriodResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = $"Retrieve list of purchases for period: From:{request.period.From} - To:{request.period.To} successful!",
                PurchasesList = result
            };

        }
    }
}
