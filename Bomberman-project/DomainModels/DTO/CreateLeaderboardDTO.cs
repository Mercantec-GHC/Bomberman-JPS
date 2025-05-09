namespace DomainModels.DTO
{
    public class CreateLeaderboardDTO
    {
        public string userName { get; set; }
        public int totalGames { get; set; }
        public int totalWins { get; set; }
        public int hightScore { get; set; }
    }
}