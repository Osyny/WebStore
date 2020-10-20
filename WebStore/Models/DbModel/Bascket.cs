using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models.DbModel
{
    public class Bascket
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public int Number { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
