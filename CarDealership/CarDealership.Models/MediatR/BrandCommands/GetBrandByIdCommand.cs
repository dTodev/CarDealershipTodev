using CarDealership.Models.Requests.BrandRequests;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record GetBrandByIdCommand(GetBrandByIdRequest brand) : IRequest<GetBrandByIdResponse>
    {
        public readonly GetBrandByIdRequest _brand = brand;    
    }
}
