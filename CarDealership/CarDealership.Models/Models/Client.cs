namespace CarDealership.Models.Users
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TotalPurchases { get; set; }
        public decimal TotalMoneySpent { get; set; }
        public DateTime LastPurchaseDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
