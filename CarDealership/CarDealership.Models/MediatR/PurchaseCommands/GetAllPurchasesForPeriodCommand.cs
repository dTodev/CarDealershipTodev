using CarDealership.Models.Requests.PurchaseRequests;
using CarDealership.Models.Responses.PurchaseResponses;
using MediatR;

namespace CarDealership.Models.MediatR.PurchaseCommands
{
    public record GetAllPurchasesForPeriodCommand(GetAllPurchasesForPeriodRequest period) : IRequest<GetAllPurchasesForPeriodResponse>
    {
        public readonly GetAllPurchasesForPeriodRequest _period = period;
    }
}
