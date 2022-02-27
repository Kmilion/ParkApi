using AutoMapper;
using ParkApi.Controllers.NationalParkController.ViewModels;
using Services.DTOs;

namespace ParkApi.Mappings
{
    public class DTOToVM : Profile
    {
        public DTOToVM()
        {
            CreateMap<NationalParkDTO, NationalParkRes>();
        }
    }
}
