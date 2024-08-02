using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    public class SongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
