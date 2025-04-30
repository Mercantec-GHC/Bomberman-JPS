using bomberman_backend.Data;
using bomberman_backend.Repository.Interfaces;
using DomainModels;

namespace bomberman_backend.Repository
{
    public class LeaderboardRepo : ILeaderboardRepo
    {
        private readonly DatabaseContextcs _databaseContext;
        public LeaderboardRepo(DatabaseContextcs databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Leaderboard CreateLeaderboard(Leaderboard leaderboard)
        {
            var leaderboardEntity = new Leaderboard
            {
                User = leaderboard.User,
                totalGames = leaderboard.totalGames,
                totalWins = leaderboard.totalWins,
                hightScore = leaderboard.hightScore
            };
            _databaseContext.leaderboards.Add(leaderboardEntity);
            _databaseContext.SaveChanges();
            return leaderboardEntity;
        }

        public Leaderboard GetLeaderboard(Guid id)
        {
            var leaderboard = _databaseContext.leaderboards.SingleOrDefault(o => o.User.UserId == id);
            if (leaderboard == null)
            {
                return null;
            }
            return leaderboard;
        }

        public List<Leaderboard> GetLeaderboards()
        {
            var leaderboards = _databaseContext.leaderboards.ToList();
            List<Leaderboard> leaderboardList = new List<Leaderboard>();
            foreach (Leaderboard leaderboard in leaderboards)
            {
                var leaderboardEntity = new Leaderboard
                {
                    User = leaderboard.User,
                    totalGames = leaderboard.totalGames,
                    totalWins = leaderboard.totalWins,
                    hightScore = leaderboard.hightScore
                };
                leaderboardList.Add(leaderboardEntity);
            }
            return leaderboardList;
        }

        public Leaderboard UpdateLeaderboard(Guid id, Leaderboard leaderboard)
        {
            var leaderboardEntity = _databaseContext.leaderboards.SingleOrDefault(o => o.User.UserId == id);
            if (leaderboardEntity == null)
            {
                return null;
            }
            leaderboardEntity.User = leaderboard.User;
            leaderboardEntity.totalGames = leaderboard.totalGames;
            leaderboardEntity.totalWins = leaderboard.totalWins;
            leaderboardEntity.hightScore = leaderboard.hightScore;
            _databaseContext.leaderboards.Update(leaderboardEntity);
            _databaseContext.SaveChanges();
            return leaderboardEntity;
        }
    }
}
