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

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rewards rewardObject)
        {

            if (ModelState.IsValid)
            {
                _db.Rewards.Add(rewardObject);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rewardObject);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var rewardFromDatabase = _db.Rewards.Find(id);

            if (rewardFromDatabase == null)
            {
                return NotFound();
            }

            return View(rewardFromDatabase);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Rewards rewardObject)
        {
            if (rewardObject.Reward == rewardObject.RewardDescription)
            {
                ModelState.AddModelError("Custom Error", "The reward name cannot be the same as the reward description.");
            }


            if (ModelState.IsValid)
            {
                _db.Rewards.Update(rewardObject);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rewardObject);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var rewardFromDatabase = _db.Rewards.Find(id);

            if (rewardFromDatabase == null)
            {
                return NotFound();
            }

            return View(rewardFromDatabase);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var rewardFromDatabase = _db.Rewards.Find(id);
            if (rewardFromDatabase == null)
            {
                return NotFound();
            }

            _db.Rewards.Remove(rewardFromDatabase);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
