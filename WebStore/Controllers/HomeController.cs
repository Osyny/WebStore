using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.Models;
using WebStore.Models.DbModel;
using WebStore.Models.ViewModel.Home;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationContext dbContext;

        private readonly UserManager<AccountUser> userManager;
        private readonly SignInManager<AccountUser> signInManager;

        public HomeController(SignInManager<AccountUser> signInManager,
            ILogger<HomeController> logger, 
            RoleManager<IdentityRole> roleManager,
            UserManager<AccountUser> userManager,
            ApplicationContext dbContext)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.signInManager = signInManager;

            this.dbContext = dbContext;
            this.userManager = userManager;
        }
       
        public async Task<IActionResult> IndexAsync()
        {
            var roles = this.roleManager.Roles.ToList();

            var userName = HttpContext.User.Identity.Name;
          
            UserViewModel model = new UserViewModel();
            int count = 0;
            if (userName != null)
            {

                AccountUser accountUser = await userManager.FindByNameAsync(userName);
                User user = this.dbContext.Userss
                    .Include(u => u.AccountUser)
                    .FirstOrDefault(u => u.AccountUserId ==Guid.Parse( accountUser.Id));

                count = this.dbContext.Basckets.Where(b => b.UserId == user.Id).ToList().Count;

                string userRoles = "";
                if (this.signInManager.IsSignedIn(User))
                {
                    if (User.Identity.IsAuthenticated)
                    {

                        if (User.IsInRole("Admin"))
                        {
                            userRoles = "Admin";
                        }
                        else if (User.IsInRole("User"))
                        {
                            userRoles = "User";
                        }
                    }
                }
                model.RoleName = userRoles;
                model.User = user;
               
                
            }
            model.CountProducts = count;

            var categorys = this.dbContext.Categories.ToList();
            model.CategoryVm = categorys.Any() ? categorys.Select(cat => new CategoryVm()
            {
                Id = cat.Id,
                Name = cat.Name,
                Img = cat.Image

            }).ToList() : new List<CategoryVm>();

            return View(model);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
