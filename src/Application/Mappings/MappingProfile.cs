using AutoMapper;
using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;

namespace WirsolutViajes.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trip, TripDTO>()
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle));


            CreateMap<City, CityDTO>();


            CreateMap<Vehicle, VehicleDTO>()
                .ForMember(dest => dest.BrandVehicleType, opt => opt.MapFrom(src => src.BrandVehicleType))
                .ForMember(dest => dest.VehicleSubtype, opt => opt.MapFrom(src => src.VehicleSubtype))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model));

            CreateMap<BrandVehicleType, BrandVehicleTypeDTO>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType));

            CreateMap<VehicleType, VehicleTypeDTO>();
            CreateMap<VehicleSubtype, VehicleSubtypeDTO>();
            CreateMap<Brand, BrandDTO>();
            CreateMap<Model, ModelDTO>();

        }
    }
}
