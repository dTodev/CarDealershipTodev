using AutoMapper;
using CarDealership.Models.Models;
using CarDealership.Models.Requests.BrandRequests;
using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Requests.ClientRequests;
using CarDealership.Models.Users;

namespace CarDealership.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CreateCarRequest, Car>();
            CreateMap<UpdateCarRequest, Car>();
            CreateMap<DeleteCarRequest, Car>();
            CreateMap<CreateBrandRequest, Brand>();
            CreateMap<UpdateBrandRequest, Brand>();
            CreateMap<DeleteBrandRequest, Brand>();
            CreateMap<CreateClientRequest, Client>();
            CreateMap<UpdateClientRequest, Client>();
            CreateMap<DeleteClientRequest, Client>();
        }
    }
}
