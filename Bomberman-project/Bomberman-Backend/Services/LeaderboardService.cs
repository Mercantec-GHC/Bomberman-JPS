using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Services.Interfaces;
using DomainModels.DTO;
using DomainModels;

namespace Bomberman_Backend.Services
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
