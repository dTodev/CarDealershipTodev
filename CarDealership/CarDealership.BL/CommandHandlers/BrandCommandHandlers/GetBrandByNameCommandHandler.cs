using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class GetBrandByNameCommandHandler : IRequestHandler<GetBrandByNameCommand, GetBrandByNameResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<GetBrandByNameCommandHandler> _logger;

        public GetBrandByNameCommandHandler(IBrandRepository brandRepository, ILogger<GetBrandByNameCommandHandler> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        public async Task<GetBrandByNameResponse> Handle(GetBrandByNameCommand request, CancellationToken cancellationToken)
        {
            var result = await _brandRepository.GetBrandByName(request._brand.Name);

            if (result == null) 
            {
                _logger.LogError($"Retrieve brand failed due to no brand with Name: {request.brand.Name} found!");

                return new GetBrandByNameResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand with such name does not exist, get operation not possible!"
                };
            }

            _logger.LogInformation($"Retrieve brand with Name: {request._brand.Name} successful!");

            return new GetBrandByNameResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved brand successfully!",
                Brand = result
            };
        }
    }
}
