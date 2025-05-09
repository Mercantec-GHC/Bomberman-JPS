using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository.Interfaces
{
    public interface ILeaderboardRepo
    {
        public List<Leaderboard> GetLeaderboards();
        public Leaderboard GetLeaderboard(string username);
        public Leaderboard CreateLeaderboard(CreateLeaderboardDTO leaderboard);
        public Leaderboard UpdateLeaderboard(CreateLeaderboardDTO leaderboard);
    }
}