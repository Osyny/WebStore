using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Models.ViewModel.Products;

namespace WebStore.Controllers.Products
{
    public class ProductController : Controller
    {
        private ApplicationContext dbContext;

        public ProductController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

       
        public ActionResult Index(Guid categoryId)
        {
            var category = this.dbContext.Categories.FirstOrDefault(cat => cat.Id == categoryId);

            var products = this.dbContext.Products.Where(pr => pr.CategoryId == category.Id).ToList();
            var model = products.Select(pr => new ProductVm()
            {
                Id = pr.Id,
                Name = pr.Name,
                Number = pr.Number,
                PriceGoods = pr.PriceGoods,
                
                CategoryId = pr.CategoryId
            }) ;
            return View(products);
        }
    }
}
