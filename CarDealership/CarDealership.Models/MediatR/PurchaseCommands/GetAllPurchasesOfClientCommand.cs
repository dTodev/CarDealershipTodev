using CarDealership.Models.Requests.PurchaseRequests;
using CarDealership.Models.Responses.PurchaseResponses;
using MediatR;

namespace CarDealership.Models.MediatR.PurchaseCommands
{
    public record GetAllPurchasesOfClientCommand(GetAllPurchasesOfClientRequest purchase) : IRequest<GetAllPurchasesOfClientResponse>
    {
        public readonly GetAllPurchasesOfClientRequest _purchase = purchase;
    }
}
