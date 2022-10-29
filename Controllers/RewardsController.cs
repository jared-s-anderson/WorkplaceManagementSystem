using Microsoft.AspNetCore.Mvc;
using WorkplaceManagementSystem.Data;
using WorkplaceManagementSystem.Models;

namespace WorkplaceManagementSystem.Controllers
{
    public class RewardsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RewardsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Rewards> rewards = _db.Rewards;
            return View(rewards);
        }
    }
}
