using bomberman_backend.Repository;
using bomberman_backend.Repository.Interfaces;
using bomberman_backend.Services.Interfaces;
using DomainModels;

namespace bomberman_backend.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardRepo _leaderboardRepo;
        public LeaderboardService(ILeaderboardRepo leaderboardRepo)
        {
            _leaderboardRepo = leaderboardRepo;
        }
        public Leaderboard CreateLeaderboard(Leaderboard leaderboard)
        {
            return _leaderboardRepo.CreateLeaderboard(leaderboard);
        }

        public Leaderboard GetLeaderboard(Guid id)
        {
            return _leaderboardRepo.GetLeaderboard(id);
        }

        public List<Leaderboard> GetLeaderboards()
        {
            return _leaderboardRepo.GetLeaderboards();
        }

        public Leaderboard UpdateLeaderboard(Guid id, Leaderboard leaderboard)
        {
            return _leaderboardRepo.UpdateLeaderboard(id, leaderboard);
        }
    }
}
