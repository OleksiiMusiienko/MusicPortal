﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<User> GetUserLog(string log)
        {
            var users = await db.Users.Where(a => a.LoginMail == log).ToListAsync();
            User? user = users?.FirstOrDefault();
            return user;
        }
        public async Task<bool> GetAdmin()
        {
            var users = await db.Users.Where(a => a.StatusAdmin == true).ToListAsync();
            if (users.Count != 0)
                return true;
            return false;
        }
        public async Task Create(User us)
        {
                await db.Users.AddAsync(us);           
            
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
        public async Task<IEnumerable<User>> GetUsersRegister()
        {
            return await db.Users.Where(a=>a.Register == false).ToListAsync();
        }
    }
}
