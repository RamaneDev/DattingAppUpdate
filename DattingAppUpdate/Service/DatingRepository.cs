using AutoMapper;
using AutoMapper.QueryableExtensions;
using DattingAppUpdate.Data;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.IService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DattingAppUpdate.Service
{
    public class DatingRepository : IDatingRepository
    {
        private readonly UserDbCxt _context;
        private readonly IMapper _mapper;

        public DatingRepository(UserDbCxt context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetPhotoAsync(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(ph => ph.Id == id);
        }

        public async Task<User> GetUserToUpdateAsync(string username)
        {
            return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.UserName == username);          
        }

        public async Task<UserToReturnDto> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                                 .Where(x => x.UserName == username)
                                 .ProjectTo<UserToReturnDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync();       
        }

        public async Task<IReadOnlyList<LightUserToReturn>> GetUsersAsync()
        {
            return  await _context.Users
                                      .ProjectTo<LightUserToReturn>(_mapper.ConfigurationProvider)
                                      .ToListAsync();         
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
