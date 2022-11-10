using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.CarResponses
{
    public class CreateCarResponse : BaseResponse
    {
        public Car Model { get; set; }
    }
}
