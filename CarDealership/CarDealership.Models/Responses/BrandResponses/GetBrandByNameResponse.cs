using CarDealership.Models.Models;

namespace CarDealership.Models.Responses.BrandResponses
{
    public class GetBrandByNameResponse : BaseResponse
    {
        public Brand Brand { get; set; }
    }
}
