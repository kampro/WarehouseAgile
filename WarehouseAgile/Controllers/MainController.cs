using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/



        public ActionResult Index()
        {
            IndexModel model = new IndexModel();

            MembershipUser user = Membership.GetUser(this.User.Identity.Name);
            if (user != null)
                model.LoggedUser = this.User.Identity.Name;

            return View(model);
        }

    }
}
