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
                Name = name

            };
            if (Image != null)
            {
                string falename = Image.FileName;
                string path = $"/files/{name}";
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

            return View(category);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, /*[Bind("Id,Avatar,LastName,Name,Surname,Sex,Birthday,CityId,Country,Region,Street,ApartmentNumber,PhoneNumber,Email,Skype,Facebook")] Search_Work.Models.ArreaDatabase.Candidate candModel,*/*/ IFormFile Image)
        {
            var upCat = this.dbContext.Categories
                .FirstOrDefault(c => c.Id == id);

            if (id != upCat.Id || upCat == null)
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
                    upCat.Image = path;
                }
            }
            upCat.Name = 

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
