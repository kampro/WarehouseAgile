using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    public class OfferController : Controller
    {
        //
        // GET: /Offer/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Offer/GetMakes

        //[ChildActionOnly] - the method is used with AJAX, it can't be ChildAction
        public ActionResult GetMakes()
        {
            MakesModel model = new MakesModel();

            return PartialView("_MakesSelect", model);
        }

        //
        // GET: /Offer/GetModels

        //[ChildActionOnly] - the method is used with AJAX, it can't be ChildAction
        public PartialViewResult GetModels()
        {
            ModelsModel model = new ModelsModel();
            int param;

            if (int.TryParse(Request.QueryString["make"], out param))
                model.FillModelsList(param);

            return PartialView("_ModelsSelect", model);
        }

        //
        // GET: /Offer/GetModelDetails

        [ChildActionOnly]
        public PartialViewResult GetModelDetails()
        {
            ModelDetailsModel model = new ModelDetailsModel();
            int param;

            if (int.TryParse(Request.QueryString["model"], out param))
                model.FillModel(param);

            return PartialView("_ModelDetails", model);
        }

        //
        // POST: /Offer/SaveModel

        [HttpPost]
        public ActionResult SaveModel()
        {
            int param;

            if (int.TryParse(Request.Form["model-id"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Model model = (from m in context.Models
                                   where m.Id == param
                                   select m).First();

                    model.Name = Request.Form["CurrentModel.Name"];
                    model.Price = float.Parse(Request.Form["CurrentModel.Price"]);

                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        //
        // GET: /Offer/GetMakeDetails

        //[ChildActionOnly]
        public ActionResult GetMakeDetails()
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
                    {
                        return Content(string.Format("Edycja {1}: <input type=\"hidden\" name=\"make-id\" value=\"{0}\"><input type=\"text\" name=\"make\" value=\"{1}\" /> <input type=\"submit\" value=\"Zapisz\" />", make.Id, make.Name));
                    }
                    else
                        new ApplicationException("Can't find Make with specified Id");
                }
            }

            return Content("null");
        }

        //
        // POST: /Offer/AddMake

        [HttpPost]
        public ActionResult AddMake()
        {
            if (Request.Form["make"].Length > 0)
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    if ((from m in context.Makes where m.Name == Request.Form["make"] select m).Count() > 0)
                        new ApplicationException("Make already exists");
                    else
                    {
                        context.Makes.Add(new Make() { Name = Request.Form["make"] });
                        context.SaveChanges();
                    }
                }
            }

            return Content("OK");
        }

        //
        // POST: /Offer/SaveMake

        [HttpPost]
        public ActionResult SaveMake()
        {
            int param;

            if (int.TryParse(Request.Form["make-id"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Make make = (from m in context.Makes
                                 where m.Id == param
                                 select m).First();

                    make.Name = Request.Form["make"];
                    context.SaveChanges();
                }
            }

            return Content("OK");
        }
    }
}
