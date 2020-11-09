using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models.ViewModel.Products
{
    public class CreateProductSubmitVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal PriceGoods { get; set; }
        public int Number { get; set; }
        public string Image { get; set; }
        public string Discription { get; set; }
        public string DiscriptionFull { get; set; }
        public Guid CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}
