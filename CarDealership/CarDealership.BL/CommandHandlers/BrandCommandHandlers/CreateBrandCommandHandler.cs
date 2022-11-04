using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreateBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBrandCommandHandler> _logger;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, ILogger<CreateBrandCommandHandler> logger)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateBrandResponse> Handle(CreateBrandCommand brandRequest, CancellationToken cancellationToken)
        {
            try
            {
                var auth = await _brandRepository.GetBrandByName(brandRequest._brand.BrandName);

                if (auth != null)
                    return new CreateBrandResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Brand already exist, creation operation is not possible."
                    };

                var brand = _mapper.Map<Brand>(brandRequest._brand);
                var result = await _brandRepository.CreateBrand(brand);

                return new CreateBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = "Create operation successful, above brand was added!",
                    BrandName = result
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Create Brand Command Handler operation error. Exception: {e}");
                throw;
            }
        }
    }
}
