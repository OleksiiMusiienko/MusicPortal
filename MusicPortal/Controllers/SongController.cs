using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.BLL.DTO;
using Portal.BLL.Interfaces;

namespace MusicPortal.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService _context;
        private readonly IGenreService _genreService;
        private readonly IWebHostEnvironment _appEnvironment;

        public SongController(ISongService context, IGenreService genreService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _genreService = genreService;
            _appEnvironment = webHostEnvironment;
        }
        public async Task <IActionResult> Index()
        {

            return View(await _context.GetAllSongs());
        }
        public async Task <IActionResult> Create()
        {
            ViewBag.Genres = new SelectList(await _genreService.GetAllGenres(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> Create(SongDTO songDTO, IFormFile uploadedFile)
        {
            var songsByAuthor = await _context.GetSongsByAuthor(songDTO.Author!);
            SongDTO sdto = songsByAuthor.Where(s=>s.Name == songDTO.Name).FirstOrDefault();

            if (sdto != null)
            {
                ModelState.AddModelError("", "Такая песня есть!");
                return View(songDTO);
            }
            else
            {
                sdto = new SongDTO();
                if(ModelState.IsValid && uploadedFile != null)
                {
                        await UploadSong(songDTO, uploadedFile);
                        await _context.Create(songDTO);
                        return View("~/Views/Song/Index.cshtml", await _context.GetAllSongs());
                                    
                }
            }
            ViewBag.Genres = new SelectList(await _genreService.GetAllGenres(), "Id", "Name", songDTO.GenreId);

            return RedirectToAction("Index");
        }

        private async Task UploadSong(SongDTO songDTO, IFormFile uploadedFile)
        {
                // Путь к папке 
                string path = "/data/" + uploadedFile.FileName; // имя файла
                songDTO.Path = path;
               
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            
        }
    }
}
