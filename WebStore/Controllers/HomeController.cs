using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Models;
using WebStore.Models.DbModel;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationContext dbContext;

        private readonly UserManager<AccountUser> userManager;

        public HomeController(
            ILogger<HomeController> logger, 
            RoleManager<IdentityRole> roleManager,
            UserManager<AccountUser> userManager,
            ApplicationContext dbContext)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public class UserViewModel
        {
            public string RoleName { get; set; }
            public AccountUser AccountUser { get; set; }
        }
        public async Task<IActionResult> IndexAsync()
        {
            var roles = this.roleManager.Roles.ToList();

            var userName = HttpContext.User.Identity.Name;
            AccountUser accountUser = await userManager.FindByNameAsync(userName);
            var model = new UserViewModel()
            {
                // RoleName = roles,

            };

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
