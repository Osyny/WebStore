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
        public AccountUser AccountUser { get; set; }
    }
}
