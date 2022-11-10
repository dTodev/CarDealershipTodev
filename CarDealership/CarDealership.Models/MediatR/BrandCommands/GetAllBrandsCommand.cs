using CarDealership.Models.Models;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record GetAllBrandsCommand : IRequest<IEnumerable<Brand>>
    {
    }
}
