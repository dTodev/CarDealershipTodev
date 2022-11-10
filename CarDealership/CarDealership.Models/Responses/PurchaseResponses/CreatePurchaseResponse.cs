using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.PurchaseResponses
{
    public class CreatePurchaseResponse : BaseResponse
    {
        public Purchase Purchase { get; set; }
    }
}
