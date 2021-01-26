﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models.DbModel;
using WebStore.Models.ViewModel.Pagination;

namespace WebStore.Models.ViewModel.Products
{
    public class ProductsViewModel
    {
        public List<ProductVm> Products { get; set; }
        public string RoleName { get; set; }
        public AccountUser AccountUser { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PriceGoods { get; set; }
        public string Name { get; set; }

    }

    public class ProductsVm
    {
        public List<ProductVm> Products { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public Guid? UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PriceGoods { get; set; }
        public string Name { get; set; }
        
        
        public string NamePriceFilter { get; set; } 

       
    }

    public class ProductVm
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal PriceGoods { get; set; }
        public string Number { get; set; }
        public string Image { get; set; }
        public string Discription { get; set; }
       
        
    
       


    }
}
