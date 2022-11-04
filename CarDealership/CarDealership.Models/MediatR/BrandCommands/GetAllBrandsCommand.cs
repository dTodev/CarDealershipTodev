using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CarDealership.Models.MediatR.BrandCommands
{
    public record GetAllBrandsCommand : IRequest<IEnumerable<Brand>>
    {
    }
}
