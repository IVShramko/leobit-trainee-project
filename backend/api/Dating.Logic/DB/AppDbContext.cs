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

        public DbSet<Chat> Chats { get; set; }

        public DbSet<ChatMember> ChatMembers { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<ChatMemberChat> ChatMemberChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasIndex(u => u.AspNetUserId)
                .IsUnique();

            modelBuilder.Entity<ChatMemberChat>()
                .HasKey(cmc => new { cmc.ChatId, cmc.ChatMemberId});

            base.OnModelCreating(modelBuilder);
        }
    }
}
