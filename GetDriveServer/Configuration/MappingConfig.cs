using AutoMapper;
using BL.DTOs;
using DAL.Models;

namespace Configuration
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, AuthDTO>().ReverseMap();
            config.CreateMap<User, RegistrationDTO>().ReverseMap();
            config.CreateMap<Ride, RideDTO>().ReverseMap();
            config.CreateMap<Review, ReviewDTO>().ReverseMap();
            config.CreateMap<PassangerDTO, UserRide> ().ReverseMap()
                .ForMember(dst => dst.Name, src => src.MapFrom(src => src.Passenger.Name))
                .ForMember(dst => dst.Email, src => src.MapFrom(src => src.Passenger.Email))
                .ForMember(dst => dst.Phone, src => src.MapFrom(src => src.Passenger.Phone));

        }
    }
}
