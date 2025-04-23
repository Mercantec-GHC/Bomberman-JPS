using System;
using DomainModels;

public class Leaderboard
{
	public Leaderboard()
	{
	}
	public int id { get; set; }
	public User User { get; set; }
	public int totalGames { get; set; }
	public int totalWins { get; set; }
	public int hightScore { get; set; }

}
