using AutoMapper;
using DataAccess.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappings
{
    public class EntityToDTO : Profile
    {
        public EntityToDTO()
        {
            CreateMap<NationalPark, NationalParkDTO>();
        }
    }
}
