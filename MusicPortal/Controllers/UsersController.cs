using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.BLL.DTO;
using Portal.BLL.Infrastructure;
using Portal.BLL.Interfaces;
using MusicPortal.Models;


namespace MusicPortal.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _context;

        public UsersController(IUserService context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllUsers());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           UserDTO us = await _context.GetUserById((int)id);
            if (us == null)
            {
                return NotFound();
            }

            return View(us);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LoginMail,Password,")] ViewUserRegister user)
        {
            var us = new UserDTO
            {
                Name = user.Name,
                LoginMail = user.LoginMail,
                Password = user.Password,
            };
            if (ModelState.IsValid)
            {
                await _context.CreateUser(us);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.GetUserById((int)id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LoginMail,StatusAdmin,Password,Salt,Register,DateReg")] UserDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateUser(user);
                }
                catch (DbUpdateConcurrencyException)
                {                   
                   return NotFound();                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.GetUserById((int)id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.GetUserById((int)id);
            if (user != null)
            {
                await _context.DeleteUser((int)id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
