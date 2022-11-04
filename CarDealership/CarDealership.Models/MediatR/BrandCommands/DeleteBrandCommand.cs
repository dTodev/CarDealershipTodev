using CarDealership.Models.Requests.BrandRequests;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record DeleteBrandCommand(DeleteBrandRequest brand) : IRequest<DeleteBrandResponse>
    {
        public readonly DeleteBrandRequest _brand = brand;
    }
}
