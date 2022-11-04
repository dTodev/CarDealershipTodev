using CarDealership.Models.Requests.BrandRequests;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record CreateBrandCommand(CreateBrandRequest brand) : IRequest<CreateBrandResponse>
    {
        public readonly CreateBrandRequest _brand = brand;
    }
}
