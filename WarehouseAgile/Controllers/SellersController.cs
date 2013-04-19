using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WarehouseAgile.Controllers
{
    public class SellersController : Controller
    {
        //
        // GET: /Sellers/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetSellers()
        {
            return PartialView("_SellersSelect");
        }

        [HttpPost]
        public ActionResult AddSeller()
        {
            MembershipCreateStatus mcs = MembershipCreateStatus.InvalidUserName;

            if (Request.Form["Username"].Length > 2
                && Request.Form["Password"].Length > 7
                && Request.Form["Name"].Length > 0
                && Request.Form["Surname"].Length > 0)
                Membership.CreateUser(Request.Form["Username"], Request.Form["Password"], "empty@empty", "Empty question", "Empty answer", true, out mcs);

            if (mcs != MembershipCreateStatus.Success)
                throw new ApplicationException("Error");
            else
            {
                Guid guid = new Guid("818a7a14-269d-4115-9f26-ac09a92f0b50");

                using (MembershipDBEntities context = new MembershipDBEntities())
                {
                    string username = Request.Form["username"];

                    var user = (from u in context.aspnet_Users
                                where u.UserName == username
                                select u).First();

                    var role = (from r in context.aspnet_Roles
                                where r.RoleId == guid
                                select r).First();

                    user.aspnet_Roles.Add(role);

                    guid = user.UserId;

                    context.SaveChanges();
                }

                using (AppDBEntities context = new AppDBEntities())
                {
                    Seller seller = new Seller();
                    seller.Id_user = guid;
                    seller.Name = Request.Form["Name"];
                    seller.Surname = Request.Form["surname"];
                    seller.Id_branch = int.Parse(Request.Form["branch"]);

                    context.Sellers.Add(seller);

                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        public ActionResult GetSellerDetails()
        {
            int param;

            if (int.TryParse(Request.QueryString["make"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Make make = (from m in context.Makes
                                 where m.Id == param
                                 select m).FirstOrDefault();

                    if (make != null)
                        return PartialView("_MakeDetails", make);
                }
            }

            return Content("<h3>Edycja</h3><div class=\"row-clean\">Brak rekordu o wskazanym identyfikatorze</div>");
        }

    }
}
