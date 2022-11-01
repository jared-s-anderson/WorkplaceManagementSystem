﻿using Microsoft.AspNetCore.Mvc;
using WorkplaceManagementSystem.Data;
using WorkplaceManagementSystem.Models;

namespace WorkplaceManagementSystem.Controllers
{
    public class EmployeeInfoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeInfoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmployeeInfo> InfoList = _db.Info;
            return View(InfoList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeInfo infoObject)
        {
            if (ModelState.IsValid)
            {
                _db.Info.Add(infoObject);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(infoObject);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var infoFromDatabase = _db.Info.Find(id);

            if (infoFromDatabase == null)
            {
                return NotFound();
            }

            return View(infoFromDatabase);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeInfo infoObject)
        {

            if (ModelState.IsValid)
            {
                _db.Info.Update(infoObject);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(infoObject);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var infoFromDatabase = _db.Info.Find(id);

            if (infoFromDatabase == null)
            {
                return NotFound();
            }

            return View(infoFromDatabase);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var infoFromDatabase = _db.Info.Find(id);
            if (infoFromDatabase == null)
            {
                return NotFound();
            }

            _db.Info.Remove(infoFromDatabase);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}