using DomainModels;

namespace bomberman_backend.Repository.Interfaces
{
    public interface ILeaderboardRepo
    {
        public List<Leaderboard> GetLeaderboards();
        public Leaderboard GetLeaderboard(Guid id);
        public Leaderboard CreateLeaderboard(Leaderboard leaderboard);
        public Leaderboard UpdateLeaderboard(Guid id, Leaderboard leaderboard);
    }
}
