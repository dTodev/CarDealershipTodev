using System.Net;
using CarDealership.BL.CommandHandlers.CarCommandHandlers;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class GetBrandByIdCommandHandler : IRequestHandler<GetBrandByIdCommand, GetBrandByIdResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<GetBrandByIdCommandHandler> _logger;

        public GetBrandByIdCommandHandler(IBrandRepository brandRepository, ILogger<GetBrandByIdCommandHandler> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        public async Task<GetBrandByIdResponse> Handle(GetBrandByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _brandRepository.GetBrandById(request._brand.Id);

            if (result == null)
            {
                _logger.LogError($"Retrieve brand failed due to no brand with ID: {request._brand.Id} found!");

                return new GetBrandByIdResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand with such ID does not exist, get operation not possible!"
                };
            }

            _logger.LogInformation($"Retrieve brand with ID: {request._brand.Id} successful!");

            return new GetBrandByIdResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved brand successfully!",
                Brand = result
            };
        }
    }
}
