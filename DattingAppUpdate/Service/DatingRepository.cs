using DattingAppUpdate.Data;
using DattingAppUpdate.Entites;
using DattingAppUpdate.IService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.Service
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

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(ph => ph.Id == id);
        }

        public async Task<User> GetUser(int id)
        {
            var usr = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return usr;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var usr = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.UserName == username);

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
