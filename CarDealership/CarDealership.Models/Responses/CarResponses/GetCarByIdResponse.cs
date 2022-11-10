using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.CarResponses
{
    public class GetCarByIdResponse : BaseResponse
    {
        public Car Id { get; set; }
    }
}
