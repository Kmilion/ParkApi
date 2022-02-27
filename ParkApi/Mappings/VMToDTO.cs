using AutoMapper;
using ParkApi.Controllers.NationalParkController.ViewModels;
using Services.DTOs;

namespace ParkApi.Mappings
{
    public class VMToDTO : Profile
    {
        public VMToDTO()
        {
            CreateMap<NationalParkReq, NationalParkDTO>();
        }
    }
}
