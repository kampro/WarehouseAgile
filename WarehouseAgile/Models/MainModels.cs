using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarehouseAgile.Models
{
    public class LayoutModel
    {
        public string LoggedUser { get; set; }
        public List<string[]> Menu
        {
            get { return this.menu; } 
        }

        private List<string[]> menu = new List<string[]>();

        public LayoutModel()
        {
            this.menu.Add(new string[] { "Zamówienia", "Index", "Orders" });
            this.menu.Add(new string[] { "Klienci", "Index", "Customers" });
            this.menu.Add(new string[] { "Oferta", "Index", "Offer" });
            this.menu.Add(new string[] { "Statystyki", "Index", "Stats" });
        }
    }
}