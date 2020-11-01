using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models.ViewModel.Products
{
    public class ProductVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal PriceGoods { get; set; }
        public int Number { get; set; }

       
        public Guid CategoryId { get; set; }
    }
}
