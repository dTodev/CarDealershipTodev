using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.BrandCommands;
using MediatR;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class GetAllBrandsCommandHandler : IRequestHandler<GetAllBrandsCommand, IEnumerable<Brand>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<Brand>> Handle(GetAllBrandsCommand request, CancellationToken cancellationToken)
        {
            return await _brandRepository.GetAllBrands();
        }
    }
}
