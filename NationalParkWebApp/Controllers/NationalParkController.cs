using Microsoft.AspNetCore.Mvc;
using NationalParkWebApp.Models;
using NationalParkWebApp.Repository.IRepository;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;

namespace NationalParkWebApp.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;

        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark nationalPark = new NationalPark();

            if (id == null)
                return View(nationalPark);

            nationalPark = await _nationalParkRepository.GetAsync(
                SD.NationalParkAPIPath,
                id.GetValueOrDefault());

            if (nationalPark == null)
                return NotFound();

            return View(nationalPark);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(
            NationalPark nationalPark,
            ICollection<IFormFile> files)
        {
            if (nationalPark == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(nationalPark);

            // Picture
            if (files != null && files.Count > 0)
            {
                byte[] p1 = null;

                using (var ms = new MemoryStream())
                {
                    using (var fs = files.FirstOrDefault().OpenReadStream())
                    {
                        fs.CopyTo(ms);
                        p1 = ms.ToArray();
                    }
                }

                nationalPark.Picture = p1;
            }
            else
            {
                var nationalParkInDb =
                    await _nationalParkRepository.GetAsync(
                        SD.NationalParkAPIPath,
                        nationalPark.Id
                    );

                if (nationalParkInDb != null)
                    nationalPark.Picture = nationalParkInDb.Picture;
                else
                    nationalPark.Picture = null;
                //nationalPark.Picture = nationalParkInDb.Picture;
            }

            if (nationalPark.Id == 0)
            {
                await _nationalParkRepository.CreateAync(
                    SD.NationalParkAPIPath,
                    nationalPark
                );
            }
            else
            {
                await _nationalParkRepository.UpdateAsync(
                    SD.NationalParkAPIPath,
                    nationalPark
                );
            }

            return RedirectToAction(nameof(Index));
        }

        #region API's

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new
            {
                data = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath)
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _nationalParkRepository.DeleteAsync(SD.NationalParkAPIPath, id);
            if (result == false)
                return Json(new { success = false, message = "Unable to delete Data!!" });

            return Json(new { success = true, message = "Deleted successfully" });
        }

        #endregion
    }
}
