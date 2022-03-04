using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkWeb.Models;
using ParkWeb.Repository.IRepository;
using System.IO;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark park = new NationalPark();

            if (id == null)
            {
                // This will be true for Insert/Create
                return View(park);
            }

            // Flow will come here for Update
            park = await _npRepo.GetAsync(SD.NationalParksAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (park == null)
            {
                return NotFound();
            }

            return View(park);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark park)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    park.Picture = p1;
                }
                else
                {
                    var objFromDb = await _npRepo.GetAsync(SD.NationalParksAPIPath, park.Id, HttpContext.Session.GetString("JWToken"));
                    park.Picture = objFromDb.Picture;
                }
                if (park.Id == 0)
                {
                    await _npRepo.CreateAsync(SD.NationalParksAPIPath, park, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _npRepo.UpdateAsync(SD.NationalParksAPIPath + park.Id, park, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(park);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await _npRepo.GetAllAsync(SD.NationalParksAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _npRepo.DeleteAsync(SD.NationalParksAPIPath, id, HttpContext.Session.GetString("JWToken")))
            {
                return Json(new { success = true, message = "Delete successful" });

            }
            return Json(new { success = false, message = "Delete not successful" });
        }
    }
}
