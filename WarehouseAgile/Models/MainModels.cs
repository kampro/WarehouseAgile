using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarehouseAgile.Models
{
    public class LayoutModel
    {
        #region Fields

        private List<string[]> menu = new List<string[]>();

        #endregion

        #region Properties

        public string LoggedUser { get; set; }

        public List<string[]> Menu
        {
            get { return this.menu; } 
        }

        #endregion

        #region Methods

        public LayoutModel()
        {
            this.menu.Add(new string[] { "Zamówienia", "Index", "Orders" });
            this.menu.Add(new string[] { "Klienci", "Index", "Customers" });
            this.menu.Add(new string[] { "Oferta", "Index", "Offer" });
            this.menu.Add(new string[] { "Statystyki", "Index", "Stats" });
            this.menu.Add(new string[] { "Sprzedawcy", "Index", "Sellers" });
        }

        #endregion
    }
}