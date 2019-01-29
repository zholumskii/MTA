using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTA.WEB.Models
{
    public class OrderWiewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public double Price { get; set; }
    }
}