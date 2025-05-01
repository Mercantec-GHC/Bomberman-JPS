using bomberman_backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace bomberman_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;
        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }
        [HttpGet]
        public ActionResult<Leaderboard> Get([FromQuery] string username)
        {
            var leaderboard = _leaderboardService.GetLeaderboard(username);
            if (Object.ReferenceEquals(leaderboard, null))
            {
                return NotFound("Leaderboard not found");
            }
            return leaderboard;
        }

        [HttpGet("/api/[controller]/all")]
        public ActionResult<List<Leaderboard>> GetAll()
        {
            var leaderboards = _leaderboardService.GetLeaderboards();
            if (Object.ReferenceEquals(leaderboards, null))
            {
                return NotFound("No leaderboards found");
            }
            return leaderboards;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateLeaderboardDTO leaderboard)
        {
            if (leaderboard == null)
            {
                return BadRequest();
            }
            var _leaderboard = _leaderboardService.CreateLeaderboard(leaderboard);
            return Ok(_leaderboard);
        }

        [HttpPut]
        public ActionResult<Leaderboard> Put([FromBody] CreateLeaderboardDTO leaderboard)
        {
            var _leaderboard = _leaderboardService.UpdateLeaderboard(leaderboard);
            if (Object.ReferenceEquals(_leaderboard, null))
            {
                return NotFound("Leaderboard not found");
            }
            return _leaderboard;
        }
    }
}
