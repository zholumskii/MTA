using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.DAL.Interfaces;
using MTA.DAL.Entities;
using MTA.DAL.EF;
using System.Data.Entity;

namespace MTA.DAL.Repositories
{
    public class ProductionRepository : IRepository<Production>
    {
        private EnterpriseContext db;

        public ProductionRepository(EnterpriseContext db)
        {
            this.db = db;
        }

        public void Create(Production item)
        {
            db.Productions.Add(item);
        }

        public void Delete(int id)
        {
            Production production = db.Productions.Find(id);

            if (production != null)
            {
                db.Productions.Remove(production);
            }
        }

        public IEnumerable<Production> Find(Func<Production, bool> predicate)
        {
            return db.Productions.Where(predicate);
        }

        public Production Get(int id)
        {
            return db.Productions.Find(id);
        }

        public IEnumerable<Production> GetAll()
        {
            return db.Productions.Include(p => p.EmployeeId);
        }

        public void Update(Production item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
