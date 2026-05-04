using Microsoft.AspNetCore.Mvc;

namespace NationalParkWebApp.Controllers
{
    public class NationalParkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
