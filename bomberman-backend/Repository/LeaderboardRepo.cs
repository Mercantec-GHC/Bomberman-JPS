using bomberman_backend.Data;
using bomberman_backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Repository
{
    public class LeaderboardRepo : ILeaderboardRepo
    {
        private readonly DatabaseContextcs _databaseContext;
        public LeaderboardRepo(DatabaseContextcs databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Leaderboard CreateLeaderboard(CreateLeaderboardDTO leaderboard)
        {
            if(string.IsNullOrEmpty(leaderboard.userName))
            {
                return null;
            }
            var leaderboardEntity = new Leaderboard
            {
                userName = leaderboard.userName,
                totalGames = leaderboard.totalGames,
                totalWins = leaderboard.totalWins,
                hightScore = leaderboard.hightScore
            };
            _databaseContext.leaderboards.Add(leaderboardEntity);
            _databaseContext.SaveChanges();
            return leaderboardEntity;
        }

        public Leaderboard GetLeaderboard(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return null;
            }
            var leaderboard = _databaseContext.leaderboards.SingleOrDefault(o => o.userName == username);
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
                    id = leaderboard.id,
                    userName = leaderboard.userName,
                    totalGames = leaderboard.totalGames,
                    totalWins = leaderboard.totalWins,
                    hightScore = leaderboard.hightScore
                };
                leaderboardList.Add(leaderboardEntity);
            }
            return leaderboardList;
        }

        public Leaderboard UpdateLeaderboard(CreateLeaderboardDTO leaderboard)
        {
            if (string.IsNullOrEmpty(leaderboard.userName))
            {
                return null;
            }
            var leaderboardEntity = _databaseContext.leaderboards.SingleOrDefault(o => o.userName == leaderboard.userName);
            if (leaderboardEntity == null)
            {
                return null;
            }
            leaderboardEntity.userName = leaderboard.userName;
            leaderboardEntity.totalGames = leaderboard.totalGames;
            leaderboardEntity.totalWins = leaderboard.totalWins;
            leaderboardEntity.hightScore = leaderboard.hightScore;
            _databaseContext.leaderboards.Update(leaderboardEntity);
            _databaseContext.SaveChanges();

            return leaderboardEntity;
        }
    }
}
