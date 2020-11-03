using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models.DbModel;

namespace WebStore.Models.ViewModel.Products
{
    public class ProductsViewModel
    {
        public List<ProductVm> Products { get; set; }
        public string RoleName { get; set; }
        public AccountUser AccountUser { get; set; }
    }
    public class ProductVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal PriceGoods { get; set; }
        public int Number { get; set; }
        public string Image { get; set; }       
        public string Discription { get; set; }       
        public Guid CategoryId { get; set; }

       
    }
}
