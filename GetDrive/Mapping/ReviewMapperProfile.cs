using AutoMapper;
using GetDrive.Models;
using GetDrive.Api;

namespace GetDrive.Mapping
{
    public class ReviewMapperProfile : Profile
    {
        public ReviewMapperProfile() 
        {
            CreateMap<ReviewDTO, ReviewListModel>().ReverseMap();
        }
    }
}
