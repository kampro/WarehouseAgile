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
        //
        // GET: /Stats/

        public ActionResult Index()
        {
            return View();
        }
    }
}
