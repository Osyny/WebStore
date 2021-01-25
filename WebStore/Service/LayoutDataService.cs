using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Models.DbModel;

namespace WebStore.Service.LayoutData
{
    public interface ILayoutDataService
    {
        Task<int> GetProductCountAsync();
        Task<Guid> UserIdAsync();
    }
    public class LayoutDataService : ILayoutDataService
    {
        private IHttpContextAccessor httpContextAccessor;
        private ApplicationContext dbContext;
        private UserManager<AccountUser> userManager;

        public LayoutDataService(ApplicationContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AccountUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<int> GetProductCountAsync()
        {
            var userName = this.httpContextAccessor.HttpContext.User.Identity.Name;
            int count = 0;
            if (userName != null)
            {

                AccountUser accountUser =  await userManager.FindByNameAsync(userName);
                User user = this.dbContext.Userss
                    .Include(u => u.AccountUser)
                    .FirstOrDefault(u => u.AccountUser == accountUser);
                if (user != null)
                {
                    count = this.dbContext.Basckets.Where(b => b.UserId == user.Id).ToList().Count;
                }
            }

            return count;
        }

        public async Task<Guid> UserIdAsync()
        {
            var user_ = this.httpContextAccessor.HttpContext.User;
            var identity = user_.Identity;
            //  var t = identity.
            var isAuth = identity.IsAuthenticated;
            string userName = identity.Name;

            var users = this.dbContext.Userss.ToList();



            Guid userId = Guid.Empty;
            if (userName != null)
            {

                AccountUser accountUser = await userManager.FindByNameAsync(userName);
                User user = this.dbContext.Userss
                   
                    .FirstOrDefault(u => u.AccountUser == accountUser);
                if (user != null)
                {
                    userId = user.Id;
                }
            }
            return userId;
            }
    }
}
