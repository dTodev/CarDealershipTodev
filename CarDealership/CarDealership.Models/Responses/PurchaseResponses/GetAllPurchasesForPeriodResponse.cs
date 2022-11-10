using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.PurchaseResponses
{
    public class GetAllPurchasesForPeriodResponse : BaseResponse
    {
        public IEnumerable<Purchase> PurchasesList { get; set; }
    }
}
