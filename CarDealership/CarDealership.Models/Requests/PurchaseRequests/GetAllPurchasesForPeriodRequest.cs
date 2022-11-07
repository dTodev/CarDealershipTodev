namespace CarDealership.Models.Requests.PurchaseRequests
{
    public class GetAllPurchasesForPeriodRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
