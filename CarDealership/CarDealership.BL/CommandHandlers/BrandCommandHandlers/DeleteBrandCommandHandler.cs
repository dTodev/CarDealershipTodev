using System.Net;
using AutoMapper;
using AutoMapper.Internal;
using CarDealership.BL.CommandHandlers.CarCommandHandlers;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteBrandCommandHandler> _logger;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, ICarRepository carRepository, IMapper mapper, ILogger<DeleteBrandCommandHandler> logger)
        {
            _brandRepository = brandRepository;
            _carRepository = carRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeleteBrandResponse> Handle(DeleteBrandCommand brandRequest, CancellationToken cancellationToken)
        {
            var brandExists = await _brandRepository.GetBrandById(brandRequest._brand.Id);

            if (brandExists == null)
            {
                _logger.LogError($"Brand removal failed due to no brand with id: {brandRequest.brand.Id} !");

                return new DeleteBrandResponse
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Message = "Brand does not exist, delete operation is not possible."
                };
            }

            var brandHasCars = await _carRepository.GetCarByBrandId(brandRequest._brand.Id);

            if (brandHasCars != null) 
            {
                _logger.LogError($"Brand with ID: {brandRequest.brand.Id} removal failed due to existing cars linked to the brand!");

                return new DeleteBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand has cars, delete operation is not possible."
                };
            }

            var result = await _brandRepository.DeleteBrand(brandRequest._brand.Id);

            _logger.LogInformation($"Brand removal successful!");

            return new DeleteBrandResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Delete operation successful, the above brand was removed!",
                Id = result
            };
        }
    }
}
