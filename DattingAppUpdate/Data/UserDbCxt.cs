using DattingAppUpdate.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DattingAppUpdate.Data
{
    public class UserDbCxt : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Likes> Likes { get; set; }

        public UserDbCxt(DbContextOptions<UserDbCxt> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);          

            builder.Entity<Photo>(entity =>
            {
                entity.HasOne(p => p.User) 
                      .WithMany(u => u.Photos)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Likes>(entity =>
            {
                entity.HasKey(l => new { l.LikerUserId, l.LikedUserId });
            });

            builder.Entity<Likes>(entity =>
            {
                entity.HasOne(u => u.LikerUser)
                      .WithMany(l => l.Liked)
                      .HasForeignKey(u => u.LikerUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Likes>(entity =>
            {
                entity.HasOne(u => u.LikedUser)
                      .WithMany(l => l.Like)
                      .HasForeignKey(u => u.LikedUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
