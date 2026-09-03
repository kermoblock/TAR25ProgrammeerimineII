using Microsoft.AspNetCore.Mvc;

namespace ShopTAR25.Controllers
{
    public class SpaceshipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
