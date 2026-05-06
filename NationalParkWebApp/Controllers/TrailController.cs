using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NationalParkWebApp.Models;
using NationalParkWebApp.Models.ViewModels;
using NationalParkWebApp.Repository.IRepository;

namespace NationalParkWebApp.Controllers
{
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalParkRepository;

        public TrailController(ITrailRepository trailRepository, INationalParkRepository nationalParkRepository)
        {
            _trailRepository = trailRepository;
            _nationalParkRepository = nationalParkRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> nationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);

            TrailVM trailVM = new TrailVM()
            {
                NationalParkList = nationalParkList.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Trail = new Trail()
            };

            if (id == null)
                return View(trailVM);

            trailVM.Trail = await _trailRepository.GetAsync(SD.TrailAPIPath, id.GetValueOrDefault());

            if (trailVM.Trail == null)
                return NotFound();

            return View(trailVM);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(TrailVM trailVM)
        {
            if (trailVM == null || trailVM.Trail == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                IEnumerable<NationalPark> nationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
                trailVM.NationalParkList = nationalParkList.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(trailVM);
            }

            if (trailVM.Trail.Id == 0)
            {
                await _trailRepository.CreateAync(SD.TrailAPIPath, trailVM.Trail);
            }
            else
            {
                await _trailRepository.UpdateAsync(
                    SD.TrailAPIPath + trailVM.Trail.Id,
                    trailVM.Trail
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
                data = await _trailRepository.GetAllAsync(SD.TrailAPIPath)
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _trailRepository.DeleteAsync(SD.TrailAPIPath, id);
            if (result == false)
                return Json(new { success = false, message = "Unable to delete Data!!" });

            return Json(new { success = true, message = "Deleted successfully" });
        }

        #endregion
    }
}