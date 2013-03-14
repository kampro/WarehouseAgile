using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarehouseAgile.Models
{
    public class CustomersModel
    {
        private List<Customer> customers;

        public List<Customer> Customers
        {
            get
            {
                return customers;
            }
        }

        public void Index()
        {
            customers = new List<Customer>();
            using (AppDBEntities db = new AppDBEntities())
            {
                foreach (Customer c in db.Customers)
                {
                    customers.Add(c);
                }
            }
        }
    }
}