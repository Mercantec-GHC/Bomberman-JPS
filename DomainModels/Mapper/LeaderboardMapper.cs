using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.DTO;

namespace DomainModels.Mapper
{
    public static class LeaderboardMapper
    {
        public static Leaderboard toCreateLeadboard(this CreateLeaderboardDTO createLeaderboardDTO)
        {
            return new Leaderboard
            {
                userName = createLeaderboardDTO.userName,
                totalGames = createLeaderboardDTO.totalGames,
                totalWins = createLeaderboardDTO.totalWins,
                hightScore = createLeaderboardDTO.hightScore
            };
        }
    }
}
