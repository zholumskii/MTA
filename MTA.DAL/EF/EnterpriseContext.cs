using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MTA.DAL.Entities;

namespace MTA.DAL.EF
{
    public class EnterpriseContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public EnterpriseContext(string connectionString) : base(connectionString) { }
    }
}
