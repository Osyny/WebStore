using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models.DbModel;

namespace WebStore.Models.ViewModel.Home
{
    public class UserViewModel
    {
        public string RoleName { get; set; }
        public User? User { get; set; }
        public int CountProducts { get; set; }
        public List<CategoryVm> CategoryVm { get; set; }
    }
    public class CategoryVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
    }
    }
