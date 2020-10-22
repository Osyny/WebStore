using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Models.DbModel;
using WebStore.Models.ViewModel;
using WebStore.Models.ViewModel.Accounts;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AccountUser> userManager;
        private readonly SignInManager<AccountUser> signInManager;
        private readonly ApplicationContext dbContext;

        public AccountController(UserManager<AccountUser> userManager,
                                 SignInManager<AccountUser> signInManager,
                                 ApplicationContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
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
                    dbContext.Userss.Add(newUser);
                    await dbContext.SaveChangesAsync();

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

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
    }
}
