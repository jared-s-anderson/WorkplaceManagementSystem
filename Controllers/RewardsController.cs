using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorkplaceManagementSystem.Data;
using WorkplaceManagementSystem.Models;

namespace WorkplaceManagementSystem.Controllers
{
    public class RewardsController : Controller
    {

        // This was added here so that I can add my data to the Rewards database set.
        private readonly ApplicationDbContext _db;

        public RewardsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]

        // Administrators and employees are given access to the rewards index view.
        // Employees have limited access to this view.
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Index()
        {

            // This was done to display data from two tables in a single view. 
            var rewards = _db.Rewards.ToList();
            var info = _db.Info.ToList();
            var rewardInfo = new RewardInfo
            {
                Rewards = rewards,
                Info = info
            };

            //IEnumerable<Rewards> rewards = _db.Rewards;
            return View(rewardInfo);
        }

        [HttpGet]

        // Only administrators can view the rewards create view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can add a reward.
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(Rewards rewardObject)
        {

            // This is a custom error that checks to see if a user enters the same thing for a reward name and description.
            if (rewardObject.Reward == rewardObject.RewardDescription)
            {
                ModelState.AddModelError("Custom Error", "The reward name cannot be the same as the reward description.");
            }

            if (ModelState.IsValid)
            {

                // The reward is added and the changes are saved.
                _db.Rewards.Add(rewardObject);
                _db.SaveChanges();

                // A message is displayed to the user, and they are redirected to the rewards index view.
                TempData["success"] = "Reward Added Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed and the user is redirected to the rewards index view.
                TempData["failure"] = "Reward Not Added";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]

        // Only administrators can access the rewards edit view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Rewards are found by their id.
            var rewardFromDatabase = _db.Rewards.Find(id);

            if (rewardFromDatabase == null)
            {
                return NotFound();
            }

            return View(rewardFromDatabase);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can edit a reward.
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Rewards rewardObject)
        {

            // This is a custom error that checks to see if a user enters the same thing for a reward name and description.
            if (rewardObject.Reward == rewardObject.RewardDescription)
            {
                ModelState.AddModelError("Custom Error", "The reward name cannot be the same as the reward description.");
            }


            if (ModelState.IsValid)
            {

                // The reward is updated, and the changes are saved.
                _db.Rewards.Update(rewardObject);
                _db.SaveChanges();

                // A message is displayed, and the user is redirected to the rewards index view.
                TempData["success"] = "Reward Updated Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed and the user is redirected to the rewards index view.
                TempData["failure"] = "Reward Not Updated";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]

        // Only administrators can access the rewards delete view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // A reward is found by its id.
            var rewardFromDatabase = _db.Rewards.Find(id);

            if (rewardFromDatabase == null)
            {
                return NotFound();
            }

            return View(rewardFromDatabase);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can delete rewards.
        [Authorize(Roles = "Administrator")]
        public IActionResult DeletePOST(int? id)
        {

            // A reward is found by its id.
            var rewardFromDatabase = _db.Rewards.Find(id);

            if (rewardFromDatabase == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {

                // The reward is removed, and the changes are saved.
                _db.Rewards.Remove(rewardFromDatabase);
                _db.SaveChanges();

                // This message is displayed, and the user is redirected to the rewards index view.
                TempData["success"] = "Reward Deleted Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed, and the user is redirected to the rewards index view.
                TempData["failure"] = "Reward Not Deleted";
                return RedirectToAction("Index");
            }
            
        }
    }
}
