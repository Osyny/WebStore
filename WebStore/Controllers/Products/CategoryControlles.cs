using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers.Products
{
    public class CategoryControlles :Controller
    {
        private readonly ApplicationContext dbContext;

        public CategoryControlles(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
