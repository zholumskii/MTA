using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTA.BLL.DTO
{
    public class ProductionDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeDTO Employee { get; set; }
    }
}
