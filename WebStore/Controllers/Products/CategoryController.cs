using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Models.DbModel;
using WebStore.Models.ViewModel.Category;

namespace WebStore.Controllers.Products
{
    public class CategoryController : Controller
    {
        private readonly ApplicationContext dbContext;
        private readonly IHostingEnvironment environment;

        public CategoryController(ApplicationContext dbContext, IHostingEnvironment environment)
        {
            this.dbContext = dbContext;
            this.environment = environment;
        }

        public ActionResult Index()
        {
            var categoryList = this.dbContext.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> CreateSubmitAsync(string name, IFormFile Image)
        {
            var newCategory = new Category()
            {
                Id = Guid.NewGuid(),
                Name = name,
                DateCreate =DateTime.Now

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

                newCategory.Image = path;
            }

            this.dbContext.Categories.Add(newCategory);
            this.dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.dbContext.Categories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var model = new CategoryEditVm()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubmit(CategoryEditVm model , IFormFile Image)
        {
            var upCat = this.dbContext.Categories
                .FirstOrDefault(c => c.Id == model.Id);

            if ( upCat == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (Image != null )
                {
                    string name = Image.FileName;
                    string path = $"/files/{name}";
                    string serverPath = $"{this.environment.WebRootPath}{path}";
                    FileStream fs = new FileStream(serverPath, FileMode.Create,
                        FileAccess.Write);
                    await Image.CopyToAsync(fs);
                    fs.Close();
                    upCat.Image = path;
                }
            }
            upCat.Name = model.Name;

            this.dbContext.Categories.Update(upCat);
            this.dbContext.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var category = this.dbContext.Categories.FirstOrDefault(cat => cat.Id == id);
            if (category != null)
            {
                var result = this.dbContext.Categories.Remove(category);
            }
            return RedirectToAction("Index");
        }
    }
}
