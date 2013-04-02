using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        #region Actions

        public ActionResult Index()
        {
            var models = new List<OrderModel>();

            using (var context = new AppDBEntities())
                context.Orders
                    .ToList()
                    .ForEach(order => models.Add(OrderModel.CreateFromEntity(order)));

            return View(models);
        }

        public ActionResult Edit(int id)
        {
            using (var context = new AppDBEntities())
                return View(OrderModel.CreateFromEntity(context.Orders
                    .Where(order => order.Id.Equals(id))
                    .FirstOrDefault()));
        }

        public ActionResult Details(int id)
        {
            using (var context = new AppDBEntities())
                return View(OrderModel.CreateFromEntity(context.Orders
                    .Where(order => order.Id.Equals(id))
                    .FirstOrDefault()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveNewOrder(int customerId, int equipmentPriceId, int colorId)
        {
            var currentUserGuid = Membership.GetUser().ProviderUserKey as Guid?;
            if (!currentUserGuid.HasValue)
                return Content("Błąd");

            using (var context = new AppDBEntities())
            {
                context.Orders.Add(new Order
                {
                    Id_car = equipmentPriceId,
                    Id_color = colorId,
                    Id_customer = customerId,
                    Id_state = 1,
                    Id_seller =
                        context.Sellers
                        .Where(seller => seller.Id_user.Value == currentUserGuid.Value)
                        .Select(seller => seller.Id).FirstOrDefault(),
                    Date = DateTime.Now
                });

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveStatus(int orderId, int statusId)
        {
            using (var context = new AppDBEntities())
            {
                var currentOrder = context.Orders.Where(order => order.Id.Equals(orderId)).FirstOrDefault();
                if (currentOrder != null)
                {
                    currentOrder.Id_state = statusId;
                    context.SaveChanges();
                }
                else
                {
                    return Content("Zapis nie powiódł się.");
                }
            }

            return RedirectToAction("Details", new { id = orderId });
        }

        [HttpPost]
        public JsonResult GetEquipmentsByModelId(int modelId)
        {
            if (modelId <= 0)
                return Json(null);

            using (var context = new AppDBEntities())
                return Json(context.EquipmentPrices
                    .Where(equipmentPrice => equipmentPrice.Id_model.Equals(modelId))
                    .Select(equipmentPrice => new
                    {
                        Id = equipmentPrice.Id,
                        Value = equipmentPrice.Equipment.Name
                    })
                    .ToList());
        }

        [HttpPost]
        public JsonResult CountOrderPrice(int modelId, int equipmentPriceId)
        {
            float price = default(float);

            if (modelId > 0)
            {
                using (var context = new AppDBEntities())
                {
                    var basicModel = context.Models.Where(model => model.Id.Equals(modelId)).FirstOrDefault();
                    if (basicModel != null)
                        price = basicModel.Price;

                    if (equipmentPriceId > 0)
                        price += context.EquipmentPrices
                            .Where(equipmentPrice => equipmentPrice.Id.Equals(equipmentPriceId))
                            .Select(equipmentPrice => equipmentPrice.Price)
                            .FirstOrDefault();
                }
            }

            return Json(price);
        }

        #endregion

        #region Public Methods

        public static IEnumerable<SelectListItem> GetStatuses()
        {
            using (var context = new AppDBEntities())
                foreach (var status in context.States)
                    yield return new SelectListItem
                    {
                        Text = status.Name,
                        Value = status.Id.ToString()
                    };
        }

        public static IEnumerable<SelectListItem> GetColors()
        {
            using (var context = new AppDBEntities())
                foreach (var color in context.Colors)
                    yield return new SelectListItem
                    {
                        Text = color.Name,
                        Value = color.Id.ToString()
                    };
        }

        public static IEnumerable<SelectListItem> GetCustomers()
        {
            using (var context = new AppDBEntities())
                foreach (var customer in context.Customers)
                    yield return new SelectListItem
                    {
                        Value = customer.Id.ToString(),
                        Text = string.Format("{0} {1} [{2}, {3}]", customer.Surname, customer.Name, customer.City, customer.Address)
                    };
        }

        #endregion
    }
}