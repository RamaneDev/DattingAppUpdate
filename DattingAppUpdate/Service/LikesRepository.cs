using DattingAppUpdate.Data;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Extensions;
using DattingAppUpdate.Helpers;
using DattingAppUpdate.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DattingAppUpdate.Service
{
    public class LikesRepository : ILikesRepository
    {
        private readonly UserDbCxt _context;

        public LikesRepository(UserDbCxt context) 
        {
            _context = context;
        }

        public async Task<Likes> GetUserLike(int likerUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(likerUserId, likedUserId);
        }
     
        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.LikerUserId == likesParams.UserId);
                users = likes.Select(like => like.LikedUser);
            }

            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.LikedUserId == likesParams.UserId);
                users = likes.Select(like => like.LikerUser);
            }

            var likeDtos = users.Select(user => new LikeDto
            {
                Username = user.UserName,
                KnownAs = user.KnowsAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City,
                Id = user.Id
            });

            return await PagedList<LikeDto>.CreateAsync(likeDtos, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(x => x.Liked)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
