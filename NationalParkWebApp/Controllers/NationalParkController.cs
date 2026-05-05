using Microsoft.AspNetCore.Mvc;
using NationalParkWebApp.Models;
using NationalParkWebApp.Repository.IRepository;

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
        public async Task<IActionResult> Upsert (int? id)
        {
            NationalPark nationalPark = new NationalPark();
            if (id == null) return View(nationalPark);
            nationalPark = await _nationalParkRepository.GetAsync(SD.NationalParkAPIPath, id.GetValueOrDefault());
            if (nationalPark == null) return NotFound();
            return View(nationalPark);
        }
        #region API's
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath) });
        }
        [HttpPost]
        public async Task<IActionResult> Upsert (NationalPark nationalPark)
        {
            if (nationalPark == null) return BadRequest();
            if (!ModelState.IsValid) return View(nationalPark);
            if (nationalPark.Id == 0) 
            await _nationalParkRepository.CreateAync(SD.NationalParkAPIPath, nationalPark);
            else
            {
                await _nationalParkRepository.UpdateAsync(SD.NationalParkAPIPath, nationalPark);
            }
            return RedirectToAction(nameof(Index));
        }
    }
        #endregion


}
