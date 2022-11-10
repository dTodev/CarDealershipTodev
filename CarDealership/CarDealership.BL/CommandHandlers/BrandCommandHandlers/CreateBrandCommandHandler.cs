using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Models;
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
            var auth = await _brandRepository.GetBrandByName(brandRequest._brand.BrandName);

            if (auth != null)
            {
                _logger.LogError($"Brand creation failed due to brand already existing in DB!");

                return new CreateBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand already exist, creation operation is not possible."
                };
            }

            var brand = _mapper.Map<Brand>(brandRequest._brand);

            var result = await _brandRepository.CreateBrand(brand);

            _logger.LogInformation($"Brand creation successful!");

            return new CreateBrandResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Create operation successful, above brand was added!",
                BrandName = result
            };
        }
    }
}
