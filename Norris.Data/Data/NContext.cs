using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Norris.Data.Data.Entities;

namespace Norris.Data.Data
{
    public class NContext : IdentityDbContext<User>
    {
        public DbSet<GameSession> GameSessions { get; set; }
        public NContext(DbContextOptions<NContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Friends>()
                .HasKey(c => new { c.UserId, c.FriendID });
            builder.Entity<Friends>()
                .HasOne(pt => pt.Friend)
                .WithMany() // <--
                .HasForeignKey(pt => pt.FriendID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Friends>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.Friends)
                .HasForeignKey(pt => pt.UserId);

            builder.Entity<GameSession>()
                .HasOne(pt => pt.PlayerBlack)
                .WithMany(pt => pt.WhiteGameSessions)
                .HasForeignKey(pt => pt.PlayerBlackID);
            builder.Entity<GameSession>()
                .HasOne(pt => pt.PlayerWhite)
                .WithMany(pt => pt.BlackGameSessions)
                .HasForeignKey(pt => pt.PlayerWhiteID)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
