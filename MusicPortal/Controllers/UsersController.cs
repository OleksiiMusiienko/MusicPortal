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
            if (HttpContext.Session.GetInt32("Admin") == null)
            {
                HttpContext.Session.Clear();
                ViewBag.UserId = null;
            }
			return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LoginMail,Password,PasswordConfirm,Register,DateReg")] RegisterModel user)
        {
            UserDTO userDto = await _context.GetUserByLog(user.LoginMail);
            if (userDto != null)
            {
                ModelState.AddModelError("LoginMail", "Такой логин занят!");
                return View(user);
            }
            userDto = new UserDTO();
            if (ModelState.IsValid)
            {
                if (user.Name == "Admin" || user.Name == "admin" || user.Name == "Administrator" || user.Name == "administrator"
                    || user.Name == "Админ" || user.Name == "Администратор")
                {
                    if (await _context.GetAdmin())
                    {
                        ModelState.AddModelError("Name", "Имя администратора использовать запрещено!");
                        return View(user);
                    }
                    else
                    {
                        userDto.Name = user.Name;
                        userDto.LoginMail = user.LoginMail;
                        userDto.Password = user.Password;
                        userDto.StatusAdmin = true;
                        userDto.Register = true;
                        await _context.CreateUser(userDto);
                        return RedirectToAction("Logon", "Users");
                    }
                }

                userDto.Name = user.Name;
                userDto.LoginMail = user.LoginMail;
                userDto.Password = user.Password;
                userDto.StatusAdmin = false;
                userDto.Register = user.Register;
                await _context.CreateUser(userDto);
                if (HttpContext.Session.GetInt32("Admin") == 1)
                {
                    return RedirectToAction("Index", "Users");
                }
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           UserDTO user = await _context.GetUserById((int)id);//запихнуть пользователя во вьюбег и поменять вьюшку на реджистер модел           
            if (user == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("Date", user.DateReg);
            if (user.StatusAdmin)
            {
                HttpContext.Session.SetInt32("Admin", 1);
            }
                EditUserModel edus = new EditUserModel
            {
                Id = user.Id,
                Name = user.Name,
                LoginMail = user.LoginMail
            };
            return View(edus);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("Id,Name,LoginMail,Password,PasswordConfirm")] EditUserModel us)
        {
            if (id != us.Id)
            {
                return NotFound();
            }
            UserDTO user = new UserDTO
            {
                Id = us.Id,
                Name = us.Name,
                Password = us.Password,
                LoginMail = us.LoginMail,
                Register = true,
                DateReg = HttpContext.Session.GetString("Date"),                
        };            
            if (HttpContext.Session.GetInt32("Admin") == 1)
            {
                user.StatusAdmin = true;
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
                return RedirectToAction("Logon");
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
                ViewBag.UserId = null;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logon(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                UserDTO udto = await _context.GetUserByLog(logon.LoginMail);
                if (udto == null)
                {
                    ModelState.AddModelError("", "Ошибка в логине или пароле!");
                    return View(logon);
                }
                else if(!udto.Register)
                {
                    ModelState.AddModelError("", "Ожидается подтверждение регистрации!");
                    return View(logon);
                }
                
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
                HttpContext.Session.SetInt32("Ident", udto.Id);
                if (udto.StatusAdmin)
                {
                    HttpContext.Session.SetInt32("Admin", 1);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }

        public async Task<IActionResult> Confirm()
        {
            return View(await _context.GetUsersRegister());
        }
        public async Task<IActionResult> RegisterConfirm(UserDTO user)
        {
            if (user == null)
            {
                return NotFound();
            }
            user.Register = true;
                try
                {
                    await _context.UpdateUser(user, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }                     
            return RedirectToAction("Confirm");
        }
        public IActionResult EditAdmin(UserDTO user)
        {           
            if (user == null)
            {
                return NotFound();
            }
            EditUserAdmin edit_user = new EditUserAdmin
            {
                Id = user.Id,
                Name = user.Name,
                LoginMail = user.LoginMail,
                StatusAdmin = user.StatusAdmin,
                Register = user.Register
            };
            
            return View(edit_user);
        }
        public async Task<IActionResult> EditUserAdmin(int id, [Bind("Id,Name,LoginMail,StatusAdmin,Register")] EditUserAdmin us)
        {
            if (id != us.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    Id = us.Id,
                    Name = us.Name,
                    LoginMail = ViewBag.User.LoginMail,
                    StatusAdmin = us.StatusAdmin,
                    Password = ViewBag.User.Password,
                    Salt = ViewBag.User.Salt,
                    Register = us.Register,
                    DateReg = ViewBag.User.DateReg
                };
               
                try
                {               
                    await _context.UpdateUser(user, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("Logon");
            }
            return RedirectToAction("Index");
        }
    }
}
