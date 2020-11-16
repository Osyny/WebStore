using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Models.DbModel;

namespace WebStore.Controllers.Products
{
    public class BasketController : Controller
    {
        private ApplicationContext dbContext;

        public BasketController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult Buy(Guid userId, Guid productId)
        {
            if(userId != null)
            {
                var user = dbContext.Userss.FirstOrDefault(user => user.Id == userId);
               
                //var bascekt = dbContext.Basckets.FirstOrDefault(b => b.UserId == user.Id);
                var newBascet = new Bascket()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    DateRegister = DateTime.Now,
                   // Number = 
                };

            }
            
            return Content("Додано в корзину!!!"); ;
        }
    }
}
