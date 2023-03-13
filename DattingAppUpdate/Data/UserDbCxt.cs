using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DattingAppUpdate.Data
{
    public class UserDbCxt : IdentityDbContext<IdentityUser>
    {
        public UserDbCxt(DbContextOptions<UserDbCxt> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
