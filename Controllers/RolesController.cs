using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Data;
using WorkplaceManagementSystem.Data;
using WorkplaceManagementSystem.Models;

namespace WorkplaceManagementSystem.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            var roles = await _roleManager.FindByIdAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            EditRolesViewModel model = new()
            {
                Id = roles.Id,
                RoleName = roles.Name,
            };


            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, roles.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRolesViewModel model)
        {
            var roles = await _roleManager.FindByIdAsync(model.Id);

            if (roles == null)
            {
                return NotFound();
            }

            else
            {
                roles.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(roles);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRoles(string? id)
        {
            var roles = await _roleManager.FindByIdAsync(id);

            ViewData["roleId"] = id;
            ViewData["roleName"] = roles.Name;

            if (roles == null)
            {
                return NotFound();
            }

            var model = new List<UserRolesModel>();

            foreach (var user in _userManager.Users)
            {
                UserRolesModel userRoles = new()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, roles.Name))
                {
                    userRoles.IsSelected = true;
                }

                else
                {
                    userRoles.IsSelected = false;
                }

                model.Add(userRoles);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoles(List<UserRolesModel> model, string? id)
        {
            var roles = await _roleManager.FindByIdAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var users = await _userManager.FindByIdAsync(model[i].UserId);

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(users, roles.Name)))
                {
                    await _userManager.AddToRoleAsync(users, roles.Name);
                }

                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(users, roles.Name))
                {
                    await _userManager.RemoveFromRoleAsync(users, roles.Name);
                }
                
                else
                {
                    continue;
                }
            }

            return RedirectToAction("Index");

        }
    }
}
