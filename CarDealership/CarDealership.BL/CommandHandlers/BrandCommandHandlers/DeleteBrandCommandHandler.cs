using System.Net;
using AutoMapper;
using CarDealership.DL.Interfaces;
using CarDealership.Models.MediatR.BrandCommands;
using CarDealership.Models.Responses.BrandResponses;
using MediatR;

namespace CarDealership.BL.CommandHandlers.BrandCommandHandlers
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, ICarRepository carRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<DeleteBrandResponse> Handle(DeleteBrandCommand brandRequest, CancellationToken cancellationToken)
        {
            var brandExists = await _brandRepository.GetBrandById(brandRequest._brand.Id);

            if (brandExists == null)
                return new DeleteBrandResponse
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Message = "Brand does not exist, delete oepration is not possible."
                };

            var brandHasCars = await _carRepository.GetCarByBrandId(brandRequest._brand.Id);

            if (brandHasCars != null)
                return new DeleteBrandResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Brand has cars, delete operation is not possible."
                };

            var result = await _brandRepository.DeleteBrand(brandRequest._brand.Id);

            return new DeleteBrandResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Delete operation successful, the above brand was removed!",
                Id = result
            };
        }
    }
}
