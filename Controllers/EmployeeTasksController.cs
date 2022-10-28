using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            IEnumerable<EmployeeTasks> taskListObject = _db.Tasks;
            return View(taskListObject);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("Index");
            }

            return View(taskObject);
        }

        // GET
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
        public IActionResult Edit(EmployeeTasks taskObject)
        {
            if (taskObject.TaskName == taskObject.TaskDescription)
            {
                ModelState.AddModelError("Custom Error", "The task name cannot be the same as the task description.");
            }


            if (ModelState.IsValid)
            {
                _db.Tasks.Update(taskObject);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskObject);
        }


        // GET
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
        public IActionResult DeletePOST(int? id)
        {
            var taskFromDatabase = _db.Tasks.Find(id);
            if (taskFromDatabase == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(taskFromDatabase);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
