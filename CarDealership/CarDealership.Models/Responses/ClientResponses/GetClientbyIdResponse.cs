using CarDealership.Models.Users;
namespace CarDealership.Models.Responses.ClientResponses
{
    public class GetClientByIdResponse : BaseResponse
    {
        public Client Id { get; set; }
    }
}
