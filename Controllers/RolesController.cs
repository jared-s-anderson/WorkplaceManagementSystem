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

        [HttpGet]

        // Only administrators can access the roles index view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]

        // Only administrators can access the roles create view.
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can create a role.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = model.RoleName
                };

                // This uses the role manager to create a new role.
                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    // If everything works correctly, this message is displayed, and the user is redirected to the roles index view.
                    TempData["success"] = "Role Created Successfully";
                    return RedirectToAction("Index");
                }

                else
                {
                    // If something goes wrong, this message is displayed, and the user is redirected to the roles index view.
                    TempData["failure"] = "Role Not Created";
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]

        // Only administrators can access the roles edit view.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string? id)
        {
            
            // A role is found by its id.
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

                // If a user is in a role, they are added to Users.
                if (await _userManager.IsInRoleAsync(user, roles.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can edit roles.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(EditRolesViewModel model)
        {

            // A role is found by its id.
            var roles = await _roleManager.FindByIdAsync(model.Id);

            if (roles == null)
            {
                return NotFound();
            }

            else
            {
                roles.Name = model.RoleName;

                // This updates a roles.
                var result = await _roleManager.UpdateAsync(roles);

                if (result.Succeeded)
                {
                    // If everything works correctly, this message is displayed, and the user is redirected to the roles index view.
                    TempData["success"] = "Role Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    // If something goes wrong, this message is displayed, and the user is redirected to the roles index view.
                    TempData["failure"] = "Role Not Updated";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]

        // Only administrators can view the edit user roles view.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditUserRoles(string? id)
        {

            // A role is found by its id.
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

                // If a user is in a role, there will be a marked check box by their name.
                if (await _userManager.IsInRoleAsync(user, roles.Name))
                {
                    userRoles.IsSelected = true;
                }

                // If a user is not in a role, there will not be a marked check box by their name.
                else
                {
                    userRoles.IsSelected = false;
                }

                model.Add(userRoles);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only administrators can edit users roles.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditUserRoles(List<UserRolesModel> model, string? id)
        {
            // A role is found by its id.
            var roles = await _roleManager.FindByIdAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {

                // Each user is found by their id.
                var users = await _userManager.FindByIdAsync(model[i].UserId);

                // When a user adds a check mark by a user's name, that user is added to that role.
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(users, roles.Name)))
                {
                    await _userManager.AddToRoleAsync(users, roles.Name);
                }

                // When a user removes a check mark by a user's name, that user is removed from that role.
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(users, roles.Name))
                {
                    await _userManager.RemoveFromRoleAsync(users, roles.Name);
                }
                
                else
                {
                    continue;
                }
            }

            if (ModelState.IsValid)
            {
                // If everything works correctly, this message is displayed, and the user is redirected to the roles index view.
                TempData["success"] = "User Roles Updated Successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If something goes wrong, this message is displayed, and the user is redirected to the roles index view.
                TempData["failure"] = "User Roles Not Updated";
                return RedirectToAction("Index");
            }

        }

        [HttpGet]

        // Only an administrator can access the roles delete view.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string? id)
        {
            // A role is found by its id.
            var roles = await _roleManager.FindByIdAsync(id);

            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Only an administrator can delete roles.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePOST(string? id)
        {
            // A role is found by its id.
            var roles = await _roleManager.FindByIdAsync(id);
            
            if (roles == null)
            {
                return NotFound();
            }

            else
            {
                // This deletes a role.
                var result = await _roleManager.DeleteAsync(roles);

                if (result.Succeeded)
                {

                    // If everything works correctly, this message will be displayed, and the user will be redirected to the roles index view.
                    TempData["success"] = "Role Deleted Successfully";
                    return RedirectToAction("Index");
                } 
                else
                {
                    // If something went wrong, this message will be displayed, and the user will be redirected to the roles index view.
                    TempData["failure"] = "Role Not Deleted";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
