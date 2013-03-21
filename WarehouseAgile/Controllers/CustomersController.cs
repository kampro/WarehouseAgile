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
                newCustomer.City = "kozia wólka";
                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Customers");
        }

    }
}
