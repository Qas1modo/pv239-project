using AutoMapper;
using BL.DTOs;
using BL.ResponseDTOs;
using DAL.Models;

namespace Configuration
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, AuthResponseDTO>().ReverseMap();
            config.CreateMap<User, RegistrationDTO>().ReverseMap();
            config.CreateMap<Ride, CreateRideDTO>().ReverseMap();
            config.CreateMap<RideDetailResponseDTO, Ride>().ReverseMap()
                .ForMember(dst => dst.DriverName, src => src.MapFrom(src => src.Driver.Name))
                .ForMember(dst => dst.DriverPhone, src => src.MapFrom(src => src.Driver.Phone))
                .ForMember(dst => dst.DriverEmail, src => src.MapFrom(src => src.Driver.Email));
            config.CreateMap<RideResponseDTO, Ride>().ReverseMap();
            config.CreateMap<Review, ReviewDTO>().ReverseMap();
            config.CreateMap<PassengerResponseDTO, UserRide>().ReverseMap()
                .ForMember(dst => dst.Name, src => src.MapFrom(src => src.Passenger.Name))
                .ForMember(dst => dst.Email, src => src.MapFrom(src => src.Passenger.Email))
                .ForMember(dst => dst.Phone, src => src.MapFrom(src => src.Passenger.Phone));
            config.CreateMap<PassengerDetailResponseDTO, UserRide>().ReverseMap()
              .ForMember(dst => dst.AvailableSeats, src => src.MapFrom(src => src.Ride.AvailableSeats))
              .ForMember(dst => dst.StartLocation, src => src.MapFrom(src => src.Ride.StartLocation))
              .ForMember(dst => dst.Destination, src => src.MapFrom(src => src.Ride.Destination))
              .ForMember(dst => dst.MaxPassengerCount, src => src.MapFrom(src => src.Ride.MaxPassengerCount))
              .ForMember(dst => dst.Price, src => src.MapFrom(src => src.Ride.Price))
              .ForMember(dst => dst.Departure, src => src.MapFrom(src => src.Ride.Departure))
              .ForMember(dst => dst.DriverNote, src => src.MapFrom(src => src.Ride.DriverNote))
              .ForMember(dst => dst.Canceled, src => src.MapFrom(src => src.Ride.Canceled));
            config.CreateMap<ReviewResponseDTO, Review>().ReverseMap()
                .ForMember(dst => dst.AuthorName, src => src.MapFrom(src => src.Author.Name));

        }
    }
}
