using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    public class OrdersController : Controller
    {
        #region Actions

        public ActionResult Index()
        {
            var models = new List<OrderViewModel>();

            using (var context = new AppDBEntities())
                context.Orders
                    .ToList()
                    .ForEach(order => models.Add(OrderViewModel.CreateFromEntity(order)));

            return View(models);
        }

        public ActionResult Edit(int orderId, int statusId)
        {
            using (var context = new AppDBEntities())
                return View(OrderViewModel.CreateFromEntity(context.Orders
                    .Where(order => order.Id.Equals(orderId))
                    .FirstOrDefault()));
        }

        public ActionResult Details(int id)
        {
            using (var context = new AppDBEntities())
                return View(OrderViewModel.CreateFromEntity(context.Orders
                    .Where(order => order.Id.Equals(id))
                    .FirstOrDefault()));
        }

        public ActionResult Create()
        {
            return View();
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

        #endregion
    }
}
