using bomberman_backend.Repository;
using bomberman_backend.Repository.Interfaces;
using bomberman_backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardRepo _leaderboardRepo;
        public LeaderboardService(ILeaderboardRepo leaderboardRepo)
        {
            _leaderboardRepo = leaderboardRepo;
        }
        public Leaderboard CreateLeaderboard(CreateLeaderboardDTO leaderboard)
        {
            return _leaderboardRepo.CreateLeaderboard(leaderboard);
        }

        public Leaderboard GetLeaderboard(string username)
        {
            return _leaderboardRepo.GetLeaderboard(username);
        }

        public List<Leaderboard> GetLeaderboards()
        {
            return _leaderboardRepo.GetLeaderboards();
        }

        public Leaderboard UpdateLeaderboard(CreateLeaderboardDTO leaderboard)
        {
            return _leaderboardRepo.UpdateLeaderboard(leaderboard);
        }
    }
}
