using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models.ViewModel.Products
{
    public class ProductEditVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string DiscriptionFull { get; set; }
        public string Image { get; set; }

        public decimal PriceGoods { get; set; }
        public string Number { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
