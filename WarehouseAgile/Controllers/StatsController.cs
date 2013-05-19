using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WarehouseAgile.Controllers
{
    [Authorize(Roles = "Boss")]
    public class StatsController : Controller
    {
        #region Actions

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cities()
        {
            return View();
        }

        public ActionResult Colors()
        {
            return View();
        }

        public ActionResult Makes()
        {
            return View();
        }

        #endregion
    }
}
