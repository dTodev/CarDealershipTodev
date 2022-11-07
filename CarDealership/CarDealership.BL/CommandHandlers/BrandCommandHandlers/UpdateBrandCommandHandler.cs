using System.Data.Odbc;
using System.Net;
using AutoMapper;
using AutoMapper.Internal;
using CarDealership.BL.CommandHandlers.CarCommandHandlers;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdateBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBrandCommandHandler> _logger;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, ILogger<UpdateBrandCommandHandler> logger)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateBrandResponse> Handle(UpdateBrandCommand brandRequest, CancellationToken cancellationToken)
        {
            var auth = await _brandRepository.GetBrandById(brandRequest._brand.Id);

            if (auth == null) 
            {
                _logger.LogError($"Brand update failed due to no brand with id: {brandRequest.brand.Id} !");

                return new UpdateBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand does not exist, update operation is not possible!"
                };
            }

            var brand = _mapper.Map<Brand>(brandRequest.brand);

            var result = await _brandRepository.UpdateBrand(brand);

            _logger.LogInformation($"Brand update successful!");

            return new UpdateBrandResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Updated brand successfully!",
                BrandName = result
            };
        }
    }
}
