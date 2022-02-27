using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.IRepository
{
    public interface ITrailsRepository
    {
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsInNationalPark(int id);
        Trail GetTrail(int id);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
        Trail GetTrail(string name);
    }
}
