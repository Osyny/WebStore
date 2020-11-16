using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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


        public ActionResult Index(Guid categoryId, string name = null, string priceGoods = null)
        {

            var category = this.dbContext.Categories.FirstOrDefault(cat => cat.Id == categoryId);

            var products = this.dbContext.Products.Where(pr => pr.CategoryId == category.Id).ToList();
            if(!string.IsNullOrEmpty(name))
            {
                var foundProd = products.Where(p => p.Name.Contains(name));
                products = foundProd.ToList();
            } 
            if(!string.IsNullOrEmpty(priceGoods))
            {
                decimal c = decimal.Parse(priceGoods);

                var foundProd = products.Where(p => p.PriceGoods == c);

                products = foundProd.ToList();
            }
            var model = new ProductsVm()
            {
                //Products =  List<ProductVm>()
                CategoryId = category.Id,
                CategoryName = category.Name,
            };
            model.Products = products.Select(pr => new ProductVm()
            {
                Id = pr.Id,
                Name = pr.Name,
                Number = pr.Number,
                PriceGoods = pr.PriceGoods,
                Image = pr.Image,
                Discription = pr.Discription,

                //CategoryId = pr.CategoryId,
                //CategoryName = category.Name,
            }).ToList() ;
            return View(model);
        }
        
        public ActionResult ProductList(Guid categoryId, string name = null, string priceGoods = null)
        {

            var userName = HttpContext.User.Identity.Name;
          
            var category = this.dbContext.Categories.FirstOrDefault(cat => cat.Id == categoryId);
            var model = new ProductsViewModel()
            {
                Products = new List<ProductVm>(),
                //CategoryId = category.Id
                CategoryName = category.Name
            };


            var products = this.dbContext.Products.Where(pr => pr.CategoryId == category.Id);
           var productList = products.OrderByDescending(p => p.Number).ToList();
            model.Products = productList.Select(pr => new ProductVm()
            {
                Id = pr.Id,
                Name = pr.Name,
                Number = pr.Number,
                PriceGoods = pr.PriceGoods,
                Image = pr.Image,

                Discription = pr.Discription,

            }).ToList();

            return View(model);
        }

        public ActionResult About(Guid id)
        {
          
            var product = this.dbContext.Products
                .Include(pr => pr.Category)
                .FirstOrDefault(pr => pr.Id == id);

            var model = new AboutProductVm()
            {
                Id= product.Id,
                CategoryName = product.Category.Name,
                Name = product.Name,
                Number = product.Number,
                Image = product.Image,
                PriceGoods = product.PriceGoods,
                Discription = product.Discription,
                DiscriptionFull = product.DiscriptionFull
            };

            return View(model);
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
           
            var scatId = Guid.Parse("959e0d03-044d-4ca2-a210-f2bba7680896");
            var symcaId = Guid.Parse("030be0f0-6d44-43a0-b278-df478caffbad");
            var shampId = Guid.Parse("23a2a828-3ba6-4403-a068-92168f0b02cb");
            var shampMyloId = Guid.Parse("4f1cadba-f733-4fe8-8aba-419131531fd6");
            var number = this.dbContext.Products.Where(p => p.CategoryId == category.Id).Count();
            if (category.Id == scatId)
            {            
                number = number + 500;
            }
            else if (category.Id == shampId)
            {
                number = number + 1000;
            } 
            else if (category.Id == shampMyloId)
            {
                number = number + 10000;
            }
           
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                PriceGoods = model.PriceGoods,
                Number = number+1,
                CategoryId = category.Id,
                Discription = model.Discription,
                DiscriptionFull = model.DiscriptionFull,
                DateCreate = DateTime.Now
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
                DiscriptionFull = product.DiscriptionFull,
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

          //  if (ModelState.IsValid)
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
            if(model.CategoryId != Guid.Empty)
            {
                upProduct.CategoryId = model.CategoryId;
            }
            
            upProduct.Discription = model.Discription;
            upProduct.DiscriptionFull = model.DiscriptionFull;
            upProduct.PriceGoods = model.PriceGoods;
           

            this.dbContext.Products.Update(upProduct);
            this.dbContext.SaveChanges();


            return RedirectToAction(nameof(ProductList), new { categoryId = upProduct.CategoryId });
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var prod = this.dbContext.Products.FirstOrDefault(cat => cat.Id == id);
            var catId = prod.CategoryId;
            if (prod != null)
            {
                var result = this.dbContext.Products.Remove(prod);
               
                this.dbContext.SaveChanges();

            }
            return RedirectToAction(nameof(ProductList), new { categoryId = catId });
        }
    }
}
