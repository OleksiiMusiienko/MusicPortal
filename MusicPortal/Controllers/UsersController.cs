using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.BLL.DTO;
using Portal.BLL.Infrastructure;
using Portal.BLL.Interfaces;
using MusicPortal.Models;
using NuGet.Protocol.Core.Types;
using System.Text;
using System.Security.Cryptography;


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
        public async Task<IActionResult> Create([Bind("Name,LoginMail,Password,PasswordConfirm")] RegisterModel user)
        {
            UserDTO udto = await _context.GetUserByLog(user.LoginMail);
            if (udto != null)
            {
               ModelState.AddModelError("", "Такой логин занят!");
                return View(user);
            }
           
            if (ModelState.IsValid)
            {
                var us = new UserDTO
                {
                    Name = user.Name,
                    LoginMail = user.LoginMail,
                    Password = user.Password,
                };   
                await _context.CreateUser(us);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Login", user.LoginMail);
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

        //авторизация пользователя
        public IActionResult Logon()
        {
            if (HttpContext.Session.GetString("Name")!= null)
            {
                HttpContext.Session.Clear();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logon(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                if(await _context.GetUserByLog(logon.LoginMail) == null)
                {
                    ModelState.AddModelError("", "Ошибка в логине или пароле!");
                    return View(logon);
                }
                UserDTO udto = await _context.GetUserByLog(logon.LoginMail);
                string? salt = udto.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (udto.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Ошибка в логине или пароле!");
                    return View(logon);
                }
                HttpContext.Session.SetString("Name", udto.Name);
                HttpContext.Session.SetString("Login", udto.LoginMail);
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }
    }
}
