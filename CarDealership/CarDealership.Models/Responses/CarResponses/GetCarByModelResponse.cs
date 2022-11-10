using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.CarResponses
{
    public class GetCarByModelResponse : BaseResponse
    {
        public Car Model { get; set; }
    }
}
