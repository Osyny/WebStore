using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models.DbModel;
using WebStore.Models.ViewModel.Roles;

namespace WebStore.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        UserManager<AccountUser> userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AccountUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(this.roleManager.Roles.ToList());
        }


        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await this.roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await this.roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(this.userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            var user = await this.userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await this.userManager.GetRolesAsync(user);
                var allRoles = this.roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            var user = await this.userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await this.userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = this.roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await this.userManager.AddToRolesAsync(user, addedRoles);

                await this.userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await this.userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(UserList));
        }
    }
}

