using DataAccess.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Managers.IManagers
{
    public interface INationalParksManager
    {
        ICollection<NationalParkDTO> GetNationalParks();
        NationalParkDTO GetNationalPark(int id);
        NationalParkDTO CreateNationalPark(NationalParkDTO parkDTO);
        void UpdateNationalPark(NationalParkDTO parkDTO);
        bool NationalParkExists(int id);
        void DeleteNationalPark(int id);
    }
}
