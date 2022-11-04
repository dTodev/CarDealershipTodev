using System.Data.Odbc;
using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdateBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<UpdateBrandResponse> Handle(UpdateBrandCommand brandRequest, CancellationToken cancellationToken)
        {
            var auth = await _brandRepository.GetBrandById(brandRequest._brand.Id);

            if (auth == null)
                return new UpdateBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand does not exist, update operation is not possible!"
                };

            var brand = _mapper.Map<Brand>(brandRequest.brand);
            var result = await _brandRepository.UpdateBrand(brand);

            return new UpdateBrandResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Updated brand successfully!",
                BrandName = result
            };
        }
    }
}
