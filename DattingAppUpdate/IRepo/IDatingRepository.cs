using DattingAppUpdate.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.IRepo
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IReadOnlyList<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByUsername(string username);
        Task<Photo> GetPhoto(int id);
    }
}
