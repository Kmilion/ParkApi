using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class TrailsRepository : ITrailsRepository
    {
        private readonly ApplicationDbContext _db;
        public TrailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateTrail(Trail trail)
        {
            _db.Trail.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.Trail.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int id)
        {
            return _db.Trail.Include(p => p.NationalPark).FirstOrDefault(n => n.Id == id);
        }

        public Trail GetTrail(string name)
        {
            return _db.Trail.Include(p => p.NationalPark).FirstOrDefault(n => n.Name.ToLower().Trim() == name.ToLower().Trim());

        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trail.Include(p => p.NationalPark).OrderBy(a => a.Name).ToList();
        }

        public bool TrailExists(string name)
        {
            return _db.Trail.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool TrailExists(int id)
        {
            return _db.Trail.Any(n => n.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateTrail(Trail trail)
        {
            Trail t = _db.Trail.FirstOrDefault(n => n.Id == trail.Id);
            t.Name = trail.Name;
            t.Distance = trail.Distance;
            t.Difficulty = trail.Difficulty;
            t.NationalParkId = trail.NationalParkId;

            _db.Trail.Update(t);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int id)
        {
            return _db.Trail.Include(p => p.NationalPark).Where(p => p.NationalPark.Id == id).ToList();
        }
    }
}
