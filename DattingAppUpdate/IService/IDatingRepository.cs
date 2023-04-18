using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.IService
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
        Task<PagedList<LightUserToReturn>> GetUsersAsync(UserParams userParams);
        Task<User> GetUserToUpdateAsync(string username);
        Task<User> GetUserToUpdateByIdAsync(int id);
        Task<UserToReturnDto> GetUserByUsernameAsync(string username);
        Task<Photo> GetPhotoAsync(int id);
    }
}
