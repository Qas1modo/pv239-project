using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GetDrive.Api;
using GetDrive.Models;
using System.Threading.Tasks;

namespace GetDrive.Mapping
{
    public class RideRequestsProfile : Profile
    {
        public RideRequestsProfile()
        {
            CreateMap<PassengerDetailResponseDTO, RideListModel>().ReverseMap();
        }
    }
}
