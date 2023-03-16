﻿using DattingAppUpdate.Entites;
using DattingAppUpdate.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly UserDbCxt _context;

        public DatingRepository(UserDbCxt context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
           var usr = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

           return usr;
        }

        public async Task<IReadOnlyList<User>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Photos).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
