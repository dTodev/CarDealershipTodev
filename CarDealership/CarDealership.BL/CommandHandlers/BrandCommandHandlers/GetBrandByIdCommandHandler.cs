using System.Net;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class GetBrandByIdCommandHandler : IRequestHandler<GetBrandByIdCommand, CreateBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;

        public GetBrandByIdCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<CreateBrandResponse> Handle(GetBrandByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _brandRepository.GetBrandById(request.brandId);

            if (result == null)
                return new CreateBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand with such ID does not exist, get operation not possible!"
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
