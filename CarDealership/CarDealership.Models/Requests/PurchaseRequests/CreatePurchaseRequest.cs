namespace CarDealership.Models.Requests.PurchaseRequests
{
    public class CreatePurchaseRequest
    {
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public decimal Price { get; set; }
    }
}
