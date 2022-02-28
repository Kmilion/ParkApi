using AutoMapper;
using ParkApi.Controllers.NationalParkController.ViewModels;
using Services.DTOs;
using System;

namespace ParkApi.Mappings
{
    public class VMToDTO : Profile
    {
        public VMToDTO()
        {
            CreateMap<NationalParkReq, NationalParkDTO>().ForMember(x => x.Picture, opt => opt.Ignore());
            CreateMap<TrailReq, TrailDTO>();
        }
    }
}
