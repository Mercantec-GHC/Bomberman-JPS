using DomainModels;

namespace bomberman_backend.Services.Interfaces
{
    public interface ILeaderboardService
    {
        public List<Leaderboard> GetLeaderboards();
        public Leaderboard GetLeaderboard(Guid id);
        public Leaderboard CreateLeaderboard(Leaderboard leaderboard);
        public Leaderboard UpdateLeaderboard(Guid id, Leaderboard leaderboard);
    }
}
