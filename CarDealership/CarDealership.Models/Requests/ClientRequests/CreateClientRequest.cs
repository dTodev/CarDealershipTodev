namespace CarDealership.Models.Requests.ClientRequests
{
    public class CreateClientRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
