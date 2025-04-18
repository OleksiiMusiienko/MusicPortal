using Microsoft.AspNetCore.Mvc;
using MusicPortal.Filters;
using MusicPortal.Models;
using System.Diagnostics;

namespace MusicPortal.Controllers
{
    [Culture]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.UserId = null;
            return RedirectToAction("Index", "Song");
        }
    }
}
