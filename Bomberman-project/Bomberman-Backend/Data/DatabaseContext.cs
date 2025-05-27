using System.Numerics;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Bomberman_Backend.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<Player> players { get; set; }
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<RefreshToken> refreshTokens { get; set; }
        public virtual DbSet<Bomb> bomb { get; set; }
        public virtual DbSet<ControllerLogs> controllerLogs { get; set; }
        public virtual DbSet<Leaderboard> leaderboards { get; set; }
        public virtual DbSet<Lobby> lobby { get; set; }
        public virtual DbSet<PowerUp> powerup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            modelBuilder.Entity<User>().UseTptMappingStrategy();
        }
    }
}