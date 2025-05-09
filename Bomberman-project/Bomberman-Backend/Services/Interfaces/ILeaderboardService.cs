using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Services.Interfaces
{
    public interface ILeaderboardService
    {
        public List<Leaderboard> GetLeaderboards();
        public Leaderboard GetLeaderboard(string username);
        public Leaderboard CreateLeaderboard(CreateLeaderboardDTO leaderboard);
        public Leaderboard UpdateLeaderboard(CreateLeaderboardDTO leaderboard);
    }
}
