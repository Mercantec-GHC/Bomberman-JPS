using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Services.Interfaces
{
    public interface ILeaderboardService
    {
        public List<Leaderboard> GetLeaderboards();
        public Leaderboard GetLeaderboard(string username);
        public Leaderboard CreateLeaderboard(CreateLeaderboardDTO leaderboard);
        public Leaderboard UpdateLeaderboard(CreateLeaderboardDTO leaderboard);
    }
}
