using Microsoft.EntityFrameworkCore;
using Portal.DAL.EF;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.DAL.Repositories
{
    public class UserRepository: IRepositoryUser
    {
        PortalContext db;
        public UserRepository (PortalContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }
        public async Task <User> GetUserId (int id)
        {
            User? user = await db.Users.FindAsync(id);
            return user!;
        }
        public async Task<User> GetUserName(string name)
        {
            var users = await db.Users.Where(a => a.Name == name).ToListAsync();
            User? user = users?.FirstOrDefault();
            return user;
        }
        public async Task<bool> GetUserLog(string log) // проверка совпадения логина
        {
            var list = await db.Users.Where(a => a.LoginMail == log).ToListAsync();
            User? user = list?.FirstOrDefault();
            if (user != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task Create(User us)
        {
            try
            {
                await db.Users.AddAsync(us);
            }
            catch 
            {
                
            }
            
        }
        public void Update(User us)
        {
            db.Entry(us).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            User? user = await db.Users.FindAsync(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }
        public void RegisterUser(User us)
        {
            if (us != null)
            {
                us.Register = true;
                Update(us);
            }
        }
    }
}
