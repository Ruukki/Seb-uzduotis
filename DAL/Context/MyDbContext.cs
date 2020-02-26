using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base()
        {
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<DbVersion> DbVersions { get; set; }

    }
}
