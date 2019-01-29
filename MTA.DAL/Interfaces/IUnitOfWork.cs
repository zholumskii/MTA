using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.DAL.Entities;

namespace MTA.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> Orders { get; }
        IRepository<Product> Products { get; }
        IRepository<OrderProduct> OrderProducts { get; }
        IRepository<Production> Productions { get; }
        IRepository<Employee> Employees { get; }
        void Save();
    }
}

