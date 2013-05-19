using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    public class CustomersController : Controller
    {
        #region AllIn
        //
        // GET: /Customers/

        public ActionResult Index()
        {
            CustomersModel model = new CustomersModel();

            model.Index();

            return View(model);
        }

        public ActionResult Add()
        {
            return View(new Customer());
        }

        [HttpPost]
        public ActionResult Add(Customer newCustomer)
        {
            using (AppDBEntities db = new AppDBEntities())
            {
                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Delete(int id)
        {
            using (AppDBEntities db = new AppDBEntities())
            {
                var orders = from a in db.Orders
                        where a.Id_customer == id
                        select a;
                Customer customer = db.Customers.Find(id);
                foreach (var item in orders)
                {
                    db.Orders.Remove(item);
                }
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            Customer customer;
            using (AppDBEntities db = new AppDBEntities())
            {
                customer = db.Customers.Find(id);
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            using (AppDBEntities db = new AppDBEntities())
            {
                Customer c = db.Customers.Find(customer.Id);
                c.Name = customer.Name;
                c.Surname = customer.Surname;
                c.Address = customer.Address;
                c.City = customer.City;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Details(int id)
        {
            Customer customer;
            using (AppDBEntities db = new AppDBEntities())
            {
                customer = db.Customers.Find(id);
            }
            return View(customer);
        }
        #endregion
    }
}
