using System.Data.SqlTypes;

namespace CarDealership.Models.Requests.ClientRequests
{
    public class UpdateClientRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
