using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models.DbModel;

namespace WebStore.Models
{
    public class ApplicationContext : IdentityDbContext<AccountUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);



            // ------------------------------------------------------
            // https://www.talkingdotnet.com/use-sql-server-sequence-in-entity-framework-core-primary-key/
            builder.HasSequence<long>("ProductNumberSeq");

            
            //builder.HasSequence<long>("DBSequence")
            //                 .StartsAt(1000).IncrementsBy(2);

            //builder.Entity<Product>()
            //   .Property(x => x.Number)
            //   .HasDefaultValueSql("NEXT VALUE FOR DBSequence");
        }

        public DbSet<User> Userss { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bascket> Basckets { get; set; }




        public int GetNextSequenceValue(string seqName)
        {
            //// var next = this.GetByRawSql($"SELECT nextval('public.\"{seqName}\"');");
             var next = this.GetByRawSql($"SELECT NEXT VALUE FOR {seqName}");

            return next;
        }


        private int GetByRawSql(string sql, params KeyValuePair<string, object>[] parameters)
        {
            int result = -1;
            var connection = this.Database.GetDbConnection();

            //try
            //{
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.Add(parameter.Value);
                }

                using (DbDataReader dataReader = command.ExecuteReader())
                    if (dataReader.HasRows)
                        while (dataReader.Read())
                            result =(int)dataReader.GetInt64(0);
            }
          
            connection.Close();

            return result;
        }

    }
}

