using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/

        public ActionResult Index()
        {
            List<OrderViewModel> models;

            using (var context = new AppDBEntities())
                models = context.Orders.Select(order => new OrderViewModel
                {
                    Branch = order.Seller.Branch.Name,
                    Car = string.Concat(order.EquipmentPrice.Model.Make.Name, " ", order.EquipmentPrice.Model.Name),
                    Color = order.Color.Name,
                    OrderDate = order.Date,
                    Price = order.EquipmentPrice.Model.Price + order.EquipmentPrice.Price,
                    Status = order.State.Name
                })
                .ToList();

            return View(models);
        }

    }
}
