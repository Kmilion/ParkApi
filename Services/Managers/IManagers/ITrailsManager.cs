using DataAccess.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Managers.IManagers
{
    public interface ITrailsManager
    {
        ICollection<TrailDTO> GetTrails();
        TrailDTO GetTrail(int id);
        TrailDTO CreateTrail(TrailDTO trailDTO);
        void UpdateTrail(TrailDTO trailDTO);
        bool TrailExists(int id);
        void DeleteTrail(int id);
    }
}
