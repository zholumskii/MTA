using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.DAL.Entities;

namespace MTA.BLL.DTO
{
    public class OrderProductDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public double Sum { get; set; }

        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }

        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
