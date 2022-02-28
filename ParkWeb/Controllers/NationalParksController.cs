using Microsoft.AspNetCore.Mvc;
using ParkWeb.Models;
using ParkWeb.Repository.IRepository;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _npRepo;

        public NationalParksController(INationalParkRepository npRepo)
        {
            _npRepo = npRepo;
        }
        public IActionResult Index()
        {
            return View(new NationalPark()
            {

            });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark park = new NationalPark();

            if (id == null)
            {
                // This will be true for Insert/Create
                return View(park);
            }

            // Flow will come here for Update
            park = await _npRepo.GetAsync(SD.NationalParksAPIPath, id.GetValueOrDefault());

            if (park == null)
            {
                return NotFound();
            }

            return View(park);
        }

        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await _npRepo.GetAllAsync(SD.NationalParksAPIPath) });
        }
    }
}
