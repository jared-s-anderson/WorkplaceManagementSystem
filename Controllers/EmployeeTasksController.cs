using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorkplaceManagementSystem.Data;
using WorkplaceManagementSystem.Models;

namespace WorkplaceManagementSystem.Controllers
{
    public class EmployeeTasksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeTasksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Index()
        {
            IEnumerable<EmployeeTasks> taskListObject = _db.Tasks;
            return View(taskListObject);
        }

        // GET
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(EmployeeTasks taskObject)
        {
            if (taskObject.TaskName == taskObject.TaskDescription)
            {
                ModelState.AddModelError("Custom Error", "The task name cannot be the same as the task description.");
            }


            if (ModelState.IsValid)
            {
                _db.Tasks.Add(taskObject);
                _db.SaveChanges();
                TempData["success"] = "Task Added Successfully";
                return RedirectToAction("Index");
            }

            return View(taskObject);
        }

        // GET
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var taskFromDatabase = _db.Tasks.Find(id);

            if (taskFromDatabase == null)
            {
                return NotFound();
            }

            return View(taskFromDatabase);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Edit(EmployeeTasks taskObject)
        {
            if (taskObject.TaskName == taskObject.TaskDescription)
            {
                ModelState.AddModelError("Custom Error", "The task name cannot be the same as the task description.");
            }


            if (ModelState.IsValid)
            {
                _db.Tasks.Update(taskObject);
                _db.Entry(taskObject).Property(x => x.TaskDate).IsModified = false;
                _db.SaveChanges();
                TempData["success"] = "Task Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(taskObject);
        }


        // GET
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var taskFromDatabase = _db.Tasks.Find(id);

            if (taskFromDatabase == null)
            {
                return NotFound();
            }

            return View(taskFromDatabase);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeletePOST(int? id)
        {
            var taskFromDatabase = _db.Tasks.Find(id);
            if (taskFromDatabase == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(taskFromDatabase);
            _db.SaveChanges();
            TempData["success"] = "Task Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
