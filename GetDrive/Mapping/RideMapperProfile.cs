using AutoMapper;
using GetDrive.Api;
using GetDrive.ViewModels.Ride;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Mapping
{
    public class RideMapperProfile : Profile
    {
        public RideMapperProfile()
        {
            CreateMap<RideResponseDTO, RideViewModel>().ReverseMap();
        }
    }
}
