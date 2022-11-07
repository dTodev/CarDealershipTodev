using CarDealership.Models.KafkaModels;
using CarDealership.Models.Requests.PurchaseRequests;
using CarDealership.Models.Responses.PurchaseResponses;
using MediatR;

namespace CarDealership.Models.MediatR.PurchaseCommands
{
    public record CreatePurchaseCommand(BasePurchase purchase) : IRequest<CreatePurchaseResponse>
    {
        public readonly BasePurchase _purchase = purchase;
    }
}
