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
    public class EmployeeRepository : IRepository<Employee>
    {
        private EnterpriseContext db;

        public EmployeeRepository(EnterpriseContext db)
        {
            this.db = db;
        }

        public void Create(Employee item)
        {
            db.Employees.Add(item);
        }

        public void Delete(int id)
        {
            Employee employee = db.Employees.Find(id);

            if (employee != null)
            {
                db.Employees.Remove(employee); 
            }
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return db.Employees.Where(predicate);
        }

        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public void Update(Employee item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
