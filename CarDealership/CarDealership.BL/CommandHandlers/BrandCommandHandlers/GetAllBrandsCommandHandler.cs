using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class GetAllBrandsCommandHandler : IRequestHandler<GetAllBrandsCommand, IEnumerable<Brand>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<GetAllBrandsCommandHandler> _logger;

        public GetAllBrandsCommandHandler(IBrandRepository brandRepository, ILogger<GetAllBrandsCommandHandler> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Brand>> Handle(GetAllBrandsCommand request, CancellationToken cancellationToken)
        {
            var result = await _brandRepository.GetAllBrands();

            if (result == null)
            {
                _logger.LogError("Retrieve all brands list error, no brands in DB!");

                return Enumerable.Empty<Brand>();
            }

            _logger.LogInformation("Retrieve all brands list successful!");

            return result;
        }
    }
}
