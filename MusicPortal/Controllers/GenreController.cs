using Microsoft.AspNetCore.Mvc;
using Portal.BLL.Interfaces;

namespace MusicPortal.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _context;
        public GenreController(IGenreService context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            return View(await _context.GetAllGenres());
        }

    }
}
