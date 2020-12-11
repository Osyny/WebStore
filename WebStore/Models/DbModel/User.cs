using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models.DbModel
{
    public class User
    {
        public Guid Id { get; set; }

        public AccountUser AccountUser { get; set; }
        //[ForeignKey("AccountUser")]
        //public string AccountUserId { get; set; }

        public DateTime DateRegister { get; set; }

    }
}
