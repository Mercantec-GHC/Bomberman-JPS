using System.Numerics;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Bomberman_Backend.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Player> players { get; set; }
        public DbSet<User> users { get; set; }

        public DbSet<Bomb> bomb { get; set; }
        public DbSet<ControllerLogs> controllerLogs { get; set; }
        public DbSet<Leaderboard> leaderboards { get; set; }
        public DbSet<Lobby> lobby { get; set; }
        public DbSet<PowerUp> powerup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTptMappingStrategy();
        }
    }
}