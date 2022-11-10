using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.PurchaseResponses
{
    public class GetAllPurchasesOfClientResponse : BaseResponse
    {
        public IEnumerable<Purchase> PurchasesList { get; set; }
    }
}
