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
            _db.Tasks.Add(taskObject);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
