using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record GetBrandByIdCommand(int brandId) : IRequest<CreateBrandResponse>
    {
    }
}
