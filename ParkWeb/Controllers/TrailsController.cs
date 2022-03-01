using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkWeb.Models;
using ParkWeb.Models.ViewModels;
using ParkWeb.Repository.IRepository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class TrailsController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        private readonly ITrailRepository _trailRepo;

        public TrailsController(INationalParkRepository npRepo, ITrailRepository trailRepo)
        {
            _npRepo = npRepo;
            _trailRepo = trailRepo;
        }
        public IActionResult Index()
        {
            return View(new Trail()
            {

            });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> npList = await _npRepo.GetAllAsync(SD.NationalParksAPIPath);

            TrailsVM objVM = new TrailsVM()
            {
                NationalParkList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Trail = new Trail()
            };


            if (id == null)
            {
                // This will be true for Insert/Create
                return View(objVM);
            }

            // Flow will come here for Update
            objVM.Trail = await _trailRepo.GetAsync(SD.TrailsAPIPath, id.GetValueOrDefault());

            if (objVM.Trail == null)
            {
                return NotFound();
            }

            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Trail.Id == 0)
                {
                    await _trailRepo.CreateAsync(SD.TrailsAPIPath, obj.Trail);
                }
                else
                {
                    await _trailRepo.UpdateAsync(SD.TrailsAPIPath + obj.Trail.Id, obj.Trail);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllTrails()
        {
            return Json(new { data = await _trailRepo.GetAllAsync(SD.TrailsAPIPath) });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _trailRepo.DeleteAsync(SD.TrailsAPIPath, id))
            {
                return Json(new { success = true, message = "Delete successful" });

            }
            return Json(new { success = false, message = "Delete not successful" });
        }
    }
}
