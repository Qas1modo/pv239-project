using AutoMapper;
using GetDrive.Models;
using GetDrive.Api;


public class AuthMapperProfile : Profile
{
    public AuthMapperProfile()
    {
        CreateMap<RegistrationDTO, RegistrationModel>().ReverseMap();
        CreateMap<LoginDto, LoginModel>().ReverseMap();
        CreateMap<ChangePasswordDTO, ChangePasswordModel>().ReverseMap();
    }
}
