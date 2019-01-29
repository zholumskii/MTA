using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.DAL.EF;
using MTA.DAL.Entities;
using MTA.DAL.Interfaces;
using System.Data.Entity;

namespace MTA.DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private EnterpriseContext db;

        public ProductRepository(EnterpriseContext db)
        {
            this.db = db;
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product product = db.Products.Find(id);

            if (product != null)
            {
                db.Products.Remove(product);
            }
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Where(predicate);
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
