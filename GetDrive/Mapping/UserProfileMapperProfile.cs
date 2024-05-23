using AutoMapper;
using GetDrive.Api;
using GetDrive.Models;

namespace GetDrive.Mapping
{
    public class UserProfileMapperProfile : Profile
    {
        public UserProfileMapperProfile()
        {

            CreateMap<ReviewResponseDTO, ReviewListModel>();
            CreateMap<RideResponseDTO, RideListModel>();
            CreateMap<PassengerDetailResponseDTO, PassengerRideListModel>();


            CreateMap<UserProfileResponseDTO, UserProfileModel>()
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.DriverRides, opt => opt.MapFrom(src => src.DriverRides))
                .ForMember(dest => dest.PassengerRides, opt => opt.MapFrom(src => src.PassengerRides));

            CreateMap<UserProfileResponseDTO, ProfileModel>()
               .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

        }
    }

}