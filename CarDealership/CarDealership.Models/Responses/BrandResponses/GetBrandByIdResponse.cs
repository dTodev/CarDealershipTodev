using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.BrandResponses
{
    public class GetBrandByIdResponse : BaseResponse
    {
        public Brand Brand { get; set; }
    }
}
