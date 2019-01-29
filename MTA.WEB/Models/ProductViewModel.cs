using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTA.WEB.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
    }
}