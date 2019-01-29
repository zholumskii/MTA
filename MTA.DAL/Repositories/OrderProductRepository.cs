using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.DAL.Entities;
using MTA.DAL.Interfaces;
using MTA.DAL.EF;
using System.Data.Entity;

namespace MTA.DAL.Repositories
{
    public class OrderProductRepository : IRepository<OrderProduct>
    {
        private EnterpriseContext db;

        public OrderProductRepository(EnterpriseContext db)
        {
            this.db = db;
        }

        public void Create(OrderProduct item)
        {
            db.OrderProducts.Add(item);
        }

        public void Delete(int id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);

            if (orderProduct != null)
            {
                db.OrderProducts.Remove(orderProduct);
            }
        }

        public IEnumerable<OrderProduct> Find(Func<OrderProduct, bool> predicate)
        {
            return db.OrderProducts.Where(predicate).ToList();
        }

        public OrderProduct Get(int id)
        {
            return db.OrderProducts.Find(id);
        }

        public IEnumerable<OrderProduct> GetAll()
        {
            return db.OrderProducts.Include(p => p.Order);
        }

        public void Update(OrderProduct item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
