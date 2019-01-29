using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTA.DAL.Entities
{
    public class Production
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
