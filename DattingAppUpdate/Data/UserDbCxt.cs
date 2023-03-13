using DattingAppUpdate.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DattingAppUpdate.Data
{
    public class UserDbCxt : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Photo> Photos { get; set; }

        public UserDbCxt(DbContextOptions<UserDbCxt> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);          

            builder.Entity<Photo>(entity =>
            {
                entity.HasOne(p => p.User)
                      .WithMany(p => p.Photos)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
