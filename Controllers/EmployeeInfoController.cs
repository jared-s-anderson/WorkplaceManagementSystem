using Microsoft.AspNetCore.Mvc;
using WorkplaceManagementSystem.Data;
using WorkplaceManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace WorkplaceManagementSystem.Controllers
{
    public class EmployeeInfoController : Controller
    {
        // I added the ApplicationDbContext here so that I can put my data in the Info database set.
        private readonly ApplicationDbContext _db;

        public EmployeeInfoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]

        // Only administrators are permitted to access the index page to view employee information.
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            IEnumerable<EmployeeInfo> InfoList = _db.Info;
            return View(InfoList);
        }

        [HttpGet]

        // Only administrators can access the employee information create view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can add employee information.
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(EmployeeInfo infoObject)
        {
            if (ModelState.IsValid)
            {

                // If everything is valid, the data is added to the Info database set, and the changes are saved.
                _db.Info.Add(infoObject);
                _db.SaveChanges();

                /* This is used to inform the user that the changed were applied successfully, and it redirects to
                 the employee info index view.*/
                TempData["success"] = "Employee Info Added Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed and the user is redirected to the employee info index view.
                TempData["failure"] = "Employee Info Not Added";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]

        // Only administrators can access the employee information edit view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // The data is found by the id of each employee and it is returned to the view.
            var infoFromDatabase = _db.Info.Find(id);

            if (infoFromDatabase == null)
            {
                return NotFound();
            }

            return View(infoFromDatabase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can update employee information.
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(EmployeeInfo infoObject)
        {

            if (ModelState.IsValid)
            {

                // The data is updated and the changes are saved.
                _db.Info.Update(infoObject);
                _db.SaveChanges();

                /* If everything worked correctly, a success message is displayed and the user is redirected
                to the employee information index view. */
                TempData["success"] = "Employee Info Updated Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed and the user is redirected to the employee info index view.
                TempData["failure"] = "Employee Info Not Updated";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]

        // Only administrators are given permission to view the employee information delete view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Employee information is found based on an id.
            var infoFromDatabase = _db.Info.Find(id);

            if (infoFromDatabase == null)
            {
                return NotFound();
            }

            return View(infoFromDatabase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // This allows administrators the ability to delete employee information.
        [Authorize(Roles = "Administrator")]
        public IActionResult DeletePOST(int? id)
        {

            // The data is found from the employee id.
            var infoFromDatabase = _db.Info.Find(id);
            if (infoFromDatabase == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // This removes the data from the Info database set and saves its changes.
                _db.Info.Remove(infoFromDatabase);
                _db.SaveChanges();

                /* If everything works out correctly, a success message is displayed and the user is
                redirected to the employee information index view. */
                TempData["success"] = "Employee Info Deleted Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed to the user and they are redirected to the employee info index view.
                TempData["failure"] = "Employee Info Not Deleted";
                return RedirectToAction("Index");
            }
        }
    }
}
