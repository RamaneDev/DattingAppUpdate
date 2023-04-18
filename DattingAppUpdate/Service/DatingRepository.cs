using AutoMapper;
using AutoMapper.QueryableExtensions;
using DattingAppUpdate.Data;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Helpers;
using DattingAppUpdate.IService;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<PagedList<LightUserToReturn>> GetUsersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                        _ => query.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<LightUserToReturn>.CreateAsync(
                    query.ProjectTo<LightUserToReturn>(_mapper.ConfigurationProvider).AsNoTracking(),
                    userParams.PageNumber,
                    userParams.PageSize                
                );         
        
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserToUpdateByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
