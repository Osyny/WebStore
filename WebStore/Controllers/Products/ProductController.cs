using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Models.DbModel;
using WebStore.Models.ViewModel.Home;
using WebStore.Models.ViewModel.Products;

namespace WebStore.Controllers.Products
{
    public class ProductController : Controller
    {
        private ApplicationContext dbContext;
        private IHostingEnvironment environment;
        //private UserManager<AccountUser> userManager;
        //private SignInManager<AccountUser> signInManager;

        public ProductController(/*UserManager<AccountUser> userManager,*/ 
            ApplicationContext dbContext,
            SignInManager<AccountUser> signInManager, IHostingEnvironment environment)
        {
            this.dbContext = dbContext;
            this.environment = environment;

            this.dbContext = dbContext;
            //this.userManager = userManager;
            //this.signInManager = signInManager;
        }

       
        public ActionResult Index(Guid categoryId)
        {
         
            var category = this.dbContext.Categories.FirstOrDefault(cat => cat.Id == categoryId);

            var products = this.dbContext.Products.Where(pr => pr.CategoryId == category.Id);
            var model = new List<ProductVm>();
            model = products.Select(pr => new ProductVm()
            {
                Id = pr.Id,
                Name = pr.Name,
                Number = pr.Number,
                PriceGoods = pr.PriceGoods,
                Image = pr.Image,

                CategoryId = pr.CategoryId
            }).ToList() ;
            return View(model);
        }
        
        public ActionResult ProductList(Guid categoryId)
        {

            var userName = HttpContext.User.Identity.Name;

             var model = new ProductsViewModel()
             {
                 Products = new List<ProductVm>()
             };
            //if (userName != null)
            //{
            //    AccountUser accountUser = await userManager.FindByNameAsync(userName);
            //    string userRoles = "";
            //    if (this.signInManager.IsSignedIn(User))
            //    {
            //        if (User.Identity.IsAuthenticated)
            //        {

            //            if (User.IsInRole("Admin"))
            //            {
            //                userRoles = "Admin";
            //            }
            //            else if (User.IsInRole("User"))
            //            {
            //                userRoles = "User";
            //            }
            //        }
            //    }
            //    model.RoleName = userRoles;
            //    model.AccountUser = accountUser;

            //}

            var category = this.dbContext.Categories.FirstOrDefault(cat => cat.Id == categoryId);

            var products = this.dbContext.Products.Where(pr => pr.CategoryId == category.Id).ToList();
            model.Products = products.Select(pr => new ProductVm()
            {
                Id = pr.Id,
                Name = pr.Name,
                Number = pr.Number,
                PriceGoods = pr.PriceGoods,
                Image = pr.Image,

                Discription = pr.Discription,

                CategoryId = pr.CategoryId
            }).ToList();
            return View(model);
        }

        public ActionResult About(Guid id)
        {
          
            var product = this.dbContext.Products.Where(pr => pr.Id == id).ToList();

            return View(product);
        }

        public IActionResult Create()
        {
            var model = new CreateProductSubmitVm()
            {
                Categories = this.dbContext.Categories.Select(cat => new SelectListItem
                {
                    Value = cat.Id.ToString(),
                    Text = cat.Name

                }).ToList()
            };
             return View(model);
        }
       

        [HttpPost]
        public async Task<IActionResult> CreateSubmit(CreateProductSubmitVm model, IFormFile Image)
        {
            var category = this.dbContext.Categories.FirstOrDefault(cat => cat.Id == model.CategoryId);
            var number = this.dbContext.Products.Count();
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                PriceGoods = model.PriceGoods,
                Number = number+1,
                CategoryId = category.Id,
                Discription = model.Discription
            };
            if (Image != null)
            {
                string falename = Image.FileName;
                string path = $"/files/{falename}";
                string serverPath = $"{this.environment.WebRootPath}{path}";
                FileStream fs = new FileStream(serverPath, FileMode.Create,
                    FileAccess.Write);
                await Image.CopyToAsync(fs);
                fs.Close();

                newProduct.Image = path;
            }

            this.dbContext.Products.Add(newProduct);
            this.dbContext.SaveChanges();

            return RedirectToAction(nameof(ProductList), new { categoryId = newProduct.CategoryId});
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categories = this.dbContext.Categories.ToList();
            var product =  this.dbContext.Products.FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductEditVm()
            {
                Id = product.Id,
                Name = product.Name,
                Image = product.Image,
                PriceGoods = product.PriceGoods,
                Discription = product.Discription,
                Number = product.Number,

                CategoryName = product.Category.Name,
                Categories = categories.Select(cat => new SelectListItem()
                {
                    Value = cat.Id.ToString(),
                    Text = cat.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubmit(ProductEditVm model, IFormFile Image)
        {
            var upProduct = this.dbContext.Products
                .FirstOrDefault(c => c.Id == model.Id);

            if (upProduct == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (Image != null)
                {
                    string name = Image.FileName;
                    string path = $"/files/{name}";
                    string serverPath = $"{this.environment.WebRootPath}{path}";
                    FileStream fs = new FileStream(serverPath, FileMode.Create,
                        FileAccess.Write);
                    await Image.CopyToAsync(fs);
                    fs.Close();
                    upProduct.Image = path;
                }
            }
            upProduct.Name = model.Name;
            upProduct.CategoryId = model.CategoriId;
            upProduct.Discription = model.Discription;
            upProduct.PriceGoods = model.PriceGoods;
           

            this.dbContext.Products.Update(upProduct);
            this.dbContext.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var prod = this.dbContext.Products.FirstOrDefault(cat => cat.Id == id);
            var catId = prod.CategoryId;
            if (prod != null)
            {
                var result = this.dbContext.Products.Remove(prod);
            }
            return RedirectToAction(nameof(ProductList), new { categoryId = catId });
        }
    }
}
