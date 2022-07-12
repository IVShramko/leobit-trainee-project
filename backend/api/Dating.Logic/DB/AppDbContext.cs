using Dating.Logic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dating.Logic.DB
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<UserAlbum> UserAlbums { get; set; }

        public DbSet<UserPhoto> UserPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasIndex(u => u.AspNetUserId)
                .IsUnique();

            //modelBuilder.Entity<UserAlbum>()
            //    .HasMany(a => a.Photos)
            //    .WithOne(p => p.Album)
            //    .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
