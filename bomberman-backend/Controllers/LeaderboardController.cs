using Microsoft.AspNetCore.Mvc;

namespace bomberman_backend.Controllers
{
    public class LeaderboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
