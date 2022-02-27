using AutoMapper;
using DataAccess.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappings
{
    public class DTOToEntity : Profile
    {
        public DTOToEntity()
        {
            CreateMap<NationalParkDTO, NationalPark>();
            CreateMap<TrailDTO, Trail>();
        }
    }
}
