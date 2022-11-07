using CarDealership.Models.Requests.BrandRequests;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record GetBrandByNameCommand(GetBrandByNameRequest brand) : IRequest<GetBrandByNameResponse>
    {
        public readonly GetBrandByNameRequest _brand = brand;
    }
}
