using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class NationalParksRepository : INationalParksRepository
    {
        private readonly ApplicationDbContext _db;
        public NationalParksRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int id)
        {
            return _db.NationalPark.FirstOrDefault(n => n.Id == id);
        }

        public NationalPark GetNationalPark(string name)
        {
            return _db.NationalPark.FirstOrDefault(n => n.Name.ToLower().Trim() == name.ToLower().Trim());

        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalPark.OrderBy(a => a.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            return _db.NationalPark.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalPark.Any(n => n.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            NationalPark np = _db.NationalPark.FirstOrDefault(n => n.Id == nationalPark.Id);
            np.Name = nationalPark.Name;
            np.State = nationalPark.State;
            np.Established = nationalPark.Established;

            _db.NationalPark.Update(np);
            return Save();
        }
    }
}
