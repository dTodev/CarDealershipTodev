using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class GetBrandByNameCommandHandler : IRequestHandler<GetBrandByNameCommand, CreateBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;

        public GetBrandByNameCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<CreateBrandResponse> Handle(GetBrandByNameCommand request, CancellationToken cancellationToken)
        {
            var result = await _brandRepository.GetBrandByName(request.brandName);

            if (result == null)
                return new CreateBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand with such name does not exist, get operation not possible!"
                };

            return new CreateBrandResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Retrieved brand successfully!",
                BrandName = result
            };
        }
    }
}
