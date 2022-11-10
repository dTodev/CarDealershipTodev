using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.CarResponses
{
    public class DeleteCarResponse : BaseResponse
    {
        public Car Id { get; set; }
    }
}
