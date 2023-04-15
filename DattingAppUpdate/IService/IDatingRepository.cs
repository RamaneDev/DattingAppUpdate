using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.IService
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
        Task<IReadOnlyList<LightUserToReturn>> GetUsersAsync();
        Task<User> GetUserToUpdateAsync(string username);
        Task<UserToReturnDto> GetUserByUsernameAsync(string username);
        Task<Photo> GetPhotoAsync(int id);
    }
}
