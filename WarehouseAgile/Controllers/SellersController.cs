using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    [Authorize(Roles = "Boss")]
    public class SellersController : Controller
    {
        //
        // GET: /Sellers/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Sellers/GetSellers

        public PartialViewResult GetSellers()
        {
            return PartialView("_SellersSelect");
        }

        //
        // POST: /Sellers/AddSeller

        [HttpPost]
        public ActionResult AddSeller()
        {
            MembershipCreateStatus mcs = MembershipCreateStatus.InvalidUserName;

            if (Request.Form["Username"].Length > 2
                && Request.Form["Password"].Length > 7
                && Request.Form["Name"].Length > 0
                && Request.Form["Surname"].Length > 0)
                Membership.CreateUser(Request.Form["Username"], Request.Form["Password"], Request.Form["Username"]+"@empty", "Empty question", "Empty answer", true, out mcs);

            if (mcs != MembershipCreateStatus.Success)
                throw new ApplicationException("Error");
            else
            {
                // Seller RoleId : 818a7a14-269d-4115-9f26-ac09a92f0b50
                Guid guid = new Guid(Request.Form["role"]);

                using (MembershipDBEntities context = new MembershipDBEntities())
                {
                    string username = Request.Form["Username"];

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
                    seller.Surname = Request.Form["Surname"];
                    seller.Id_branch = int.Parse(Request.Form["branch"]);

                    context.Sellers.Add(seller);

                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        //
        // GET: /Sellers/GetSellerDetails

        public ActionResult GetSellerDetails()
        {
            if (Request.QueryString["seller"] != "0")
            {
                Guid guid = new Guid(Request.QueryString["seller"]);

                using (AppDBEntities context = new AppDBEntities())
                {
                    Seller seller = (from s in context.Sellers
                                     where s.Id_user == guid
                                     select s).FirstOrDefault();

                    SellerModel model = new SellerModel();
                    model.UserId = (Guid)seller.Id_user;
                    model.Name = seller.Name;
                    model.Surname = seller.Surname;

                    using (MembershipDBEntities context2 = new MembershipDBEntities())
                    {
                        var user = (from s in context2.aspnet_Users
                                    where s.UserId == guid
                                    select s).FirstOrDefault();

                        model.Username = user.UserName;
                    }

                    if (seller != null)
                        return PartialView("_SellerDetails", model);
                }
            }

            return Content("<h3>Edycja</h3><div class=\"row-clean\">Brak rekordu o wskazanym identyfikatorze</div>");
        }

        //
        // POST: /Sellers/SaveSeller

        [HttpPost]
        public ActionResult SaveSeller()
        {
            Guid guid = new Guid(Request.Form["seller-id"]);

            using (MembershipDBEntities context = new MembershipDBEntities())
            {
                string username = Request.Form["username"];
                string oldUsername = (from u in context.aspnet_Users where u.UserId == guid select u.UserName).First();

                if (username != oldUsername && (from u in context.aspnet_Users where u.UserName == username select u).Any())
                    throw new ApplicationException("User already exists");
                else
                {

                    var user = (from u in context.aspnet_Users
                                where u.UserId == guid
                                select u).First();

                    user.UserName = username;
                    user.LoweredUserName = username.ToLower();

                    Guid roleGuid = new Guid(Request.Form["role"]);

                    var role = (from r in context.aspnet_Roles
                                where r.RoleId == roleGuid
                                select r).First();

                    user.aspnet_Roles.Clear();
                    user.aspnet_Roles.Add(role);

                    context.SaveChanges();
                }
            }

            using (AppDBEntities context = new AppDBEntities())
            {
                Seller seller = (from s in context.Sellers
                                 where s.Id_user == guid
                                 select s).First();

                seller.Name = Request.Form["Name"];
                seller.Surname = Request.Form["Surname"];
                seller.Id_branch = int.Parse(Request.Form["branch"]);

                context.SaveChanges();
            }

            return Content("OK");
        }

        //
        // GET: /Sellers/DeleteSeller

        public ActionResult DeleteSeller()
        {
            Guid guid = new Guid(Request.QueryString["seller"]);

            using (MembershipDBEntities context = new MembershipDBEntities())
            {
                var user = (from u in context.aspnet_Users
                            where u.UserId == guid
                            select u).First();

                Membership.DeleteUser(user.UserName);
            }
            
            using (AppDBEntities context = new AppDBEntities())
            {
                Seller seller = (from s in context.Sellers
                                 where s.Id_user == guid
                                 select s).First();

                context.Sellers.Remove(seller);

                context.SaveChanges();
            }

            return Content("OK");
        }

    }
}
