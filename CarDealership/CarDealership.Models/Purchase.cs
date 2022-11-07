namespace CarDealership.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
    }
}
