using CarDealership.Models.Users;
namespace CarDealership.Models.Responses.ClientResponses
{
    public class GetClientByNameResponse : BaseResponse
    {
        public Client Name { get; set; }
    }
}
