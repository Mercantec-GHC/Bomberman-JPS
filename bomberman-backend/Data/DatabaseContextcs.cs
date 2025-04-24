using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace bomberman_backend.Data
{
    public class DatabaseContextcs : DbContext
    {
        public DatabaseContextcs(DbContextOptions<DatabaseContextcs> options) : base(options) { }

       public DbSet<Player> players { get; set; }
       public DbSet<User> users { get; set; }

        public DbSet<Bomb> bomb { get; set; }
        public DbSet<ControllerLogs> controllerLogs { get; set; }
        public DbSet<Leaderboard> leaderboards { get; set; }
        public DbSet<Lobby> lobby { get; set; }
        public DbSet<PowerUp> powerup { get; set; }
    }
}
