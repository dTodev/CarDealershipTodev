using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record GetBrandByNameCommand(string brandName) : IRequest<CreateBrandResponse>
    {
    }
}
