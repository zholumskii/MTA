using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.DAL.EF;
using MTA.DAL.Entities;
using MTA.DAL.Interfaces;

namespace MTA.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EnterpriseContext db;

        private OrderRepository orderRepository;
        private ProductRepository productRepository;
        private OrderProductRepository orderProductRepository;
        private ProductionRepository productionRepository;
        private EmployeeRepository employeeRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new EnterpriseContext(connectionString);
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(db);
                }
                return orderRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(db);
                }
                return productRepository;
            }
        }

        public IRepository<OrderProduct> OrderProducts
        {
            get
            {
                if (orderProductRepository == null)
                {
                    orderProductRepository = new OrderProductRepository(db);
                }
                return orderProductRepository;
            }
        }

        public IRepository<Production> Productions
        {
            get
            {
                if (productionRepository == null)
                {
                    productionRepository = new ProductionRepository(db);
                }
                return productionRepository;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new EmployeeRepository(db);
                }
                return employeeRepository;
            }
        }

        private bool desposed = false;

        public virtual void Dispose(bool desposing)
        {
            if (!desposed)
            {
                if (desposing)
                {
                    db.Dispose();
                }
                desposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
