using CarDealership.Models.Requests.BrandRequests;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record UpdateBrandCommand(UpdateBrandRequest brand) : IRequest<UpdateBrandResponse>
    {
        public readonly UpdateBrandRequest _brand = brand;
    }
}
