using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models.DbModel;
using WebStore.Models.ViewModel;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AccountUser> userManager;
        private readonly SignInManager<AccountUser> signInManager;
        public AccountController(UserManager<AccountUser> userManager,
                                 SignInManager<AccountUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Views/Account/Register.chtml");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {

            if(ModelState.IsValid)
            {
                AccountUser accountUser = new AccountUser { Email = model.Email, UserName = model.Email };
                // add
                var res = await this.userManager.CreateAsync(accountUser, model.Password);
                if(res.Succeeded)
                {
                    // установка куки
                    await this.signInManager.SignInAsync(accountUser, false);

                    var newUser = new User()
                    {
                        Id = Guid.NewGuid(),
                        AccountUser = accountUser,
                        DateRegister = DateTime.Now
                        
                    };

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("Views/Account/Register.chtml", model);
        }
    }
}
