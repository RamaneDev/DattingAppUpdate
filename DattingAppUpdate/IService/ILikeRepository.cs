using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Helpers;
using System.Threading.Tasks;

namespace DattingAppUpdate.IService
{
    public interface ILikesRepository
    {
        Task<Likes> GetUserLike(int likerUserId, int likedUserId);
        Task<User> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}
