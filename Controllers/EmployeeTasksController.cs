using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorkplaceManagementSystem.Data;
using WorkplaceManagementSystem.Models;

namespace WorkplaceManagementSystem.Controllers
{
    public class EmployeeTasksController : Controller
    {

        // This was added here so that I can add my data to the Tasks database set.
        private readonly ApplicationDbContext _db;

        public EmployeeTasksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]

        // Both administrators and employees are given access to view the employee tasks index view.
        // Only certain parts of the view will show up for employees because they have limited access.
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Index()
        {
            IEnumerable<EmployeeTasks> taskListObject = _db.Tasks;
            return View(taskListObject);
        }

        [HttpGet]

        // Only the administrator can view the employee tasks create view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can create an employee task.
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(EmployeeTasks taskObject)
        {

            /* I added this custom error that will alert a user if they try to enter the same information for the task
            name and description. */
            if (taskObject.TaskName == taskObject.TaskDescription)
            {
                ModelState.AddModelError("Custom Error", "The task name cannot be the same as the task description.");
            }


            if (ModelState.IsValid)
            {

                // If everything is valid, the data is added to the Tasks database set, and the changes are saved.
                _db.Tasks.Add(taskObject);
                _db.SaveChanges();

                // A success message will be displayed, and the user will be redirected to the employee tasks index view.
                TempData["success"] = "Task Added Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed and the user is redirected to the employee tasks index view.
                TempData["failure"] = "Task Not Added";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]

        // Both administrators and employees can view the employee tasks edit view.
        // Employees will only see certain parts of the view because they do not have full access.
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Each tasks is found by its id.
            var taskFromDatabase = _db.Tasks.Find(id);

            if (taskFromDatabase == null)
            {
                return NotFound();
            }

            return View(taskFromDatabase);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Employees and administrators have different levels of access to edit the tasks.
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Edit(EmployeeTasks taskObject)
        {

            /* I added this custom error to make sure that a user does not enter the same information for the task name
            and description. */
            if (taskObject.TaskName == taskObject.TaskDescription)
            {
                ModelState.AddModelError("Custom Error", "The task name cannot be the same as the task description.");
            }


            if (ModelState.IsValid)
            {

                // If everything works correctly, the data is updated and saved.
                _db.Tasks.Update(taskObject);

                // I added this to make sure that the task date does not change.
                _db.Entry(taskObject).Property(x => x.TaskDate).IsModified = false;
                _db.SaveChanges();

                // A success message is displayed and the user is redirected to the employee tasks index view.
                TempData["success"] = "Task Updated Successfully";
                return RedirectToAction("Index");
            }

            else
            {

                // If something goes wrong, this message is displayed and the user is redirected to the employee tasks index view.
                TempData["failure"] = "Task Not Updated";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]

        // Only administrators can view the employee tasks delete view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Tasks are found by their id.
            var taskFromDatabase = _db.Tasks.Find(id);

            if (taskFromDatabase == null)
            {
                return NotFound();
            }

            return View(taskFromDatabase);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Administrators can delete employee tasks.
        [Authorize(Roles = "Administrator")]
        public IActionResult DeletePOST(int? id)
        {
            // Tasks are found by their id.
            var taskFromDatabase = _db.Tasks.Find(id);
            if (taskFromDatabase == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                // A task is removed, and the changes are saved.
                _db.Tasks.Remove(taskFromDatabase);
                _db.SaveChanges();

                /* A success message is displayed and the user is redirected to the employee tasks
                index view */
                TempData["success"] = "Task Deleted Successfully";
                return RedirectToAction("Index");
            }
            
            else
            {

                // If something goes wrong, this message is displayed and the user is redirected to the employee tasks index view.
                TempData["failure"] = "Tasks Not Deleted";
                return RedirectToAction("Index");
            }
        }
    }
}
