using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.CarResponses
{
    public class UpdateCarResponse : BaseResponse
    {
        public Car Model { get; set; }
    }
}
