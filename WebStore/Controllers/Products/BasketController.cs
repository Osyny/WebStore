﻿using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Index(Guid userId)
        {
            var userBaskets = this.dbContext.Basckets.Where(b => b.UserId == userId).ToList();
            return View(userBaskets);
        }
        [HttpPost]
        public IActionResult Buy(Guid userId, Guid productId)
        {
            var product = dbContext.Products.FirstOrDefault(pr => pr.Id == productId);

            var newBascet = new Bascket()
            {
                Id = Guid.NewGuid(),
                DateRegister = DateTime.Now,
                // Number = 
                ProductId = product.Id
            };
            if (userId != null)
            {
                var user = dbContext.Userss.FirstOrDefault(user => user.Id == userId);
                       
                
            }
            this.dbContext.Basckets.Add(newBascet);
            this.dbContext.SaveChanges();
          


            return Content("Додано в корзину!!!"); ;
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var bascket = this.dbContext.Basckets.FirstOrDefault(b => b.Id == id);
            if (bascket != null)
            {
                var result = this.dbContext.Basckets.Remove(bascket);
            }
            return RedirectToAction("Index");
        }
    }
}
