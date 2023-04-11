using DattingAppUpdate.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DattingAppUpdate.Extensions
{
    public static class Extensions
    {

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;

            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;

        }

        public async static Task<User> GetUserByNameWithPhotos(this UserManager<User> usermanger, string username)
        {
            return await usermanger.Users.Include(u => u.Photos)
                                         .SingleOrDefaultAsync(x => x.UserName == username);

        }

        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
