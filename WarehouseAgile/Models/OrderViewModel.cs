using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarehouseAgile.Models
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public string Car { get; set; }
        public string Branch { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
    }
}