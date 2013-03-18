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

        [ChildActionOnly]
        public ActionResult GetMakes()
        {
            MakesModel model = new MakesModel();

            return PartialView("_MakesSelect", model);
        }

        //
        // GET: /Offer/GetModels

        public PartialViewResult GetModels()
        {
            ModelsModel model = new ModelsModel();

            int param;
            if (int.TryParse(Request.QueryString["make"], out param))
                model.FillModelsList(param);

            return PartialView("_Models", model);
        }

        //
        // GET: /Offer/GetModelPrices

        public PartialViewResult GetModelPrices()
        {
            ModelPricesModel model = new ModelPricesModel();

            int param;
            if (int.TryParse(Request.QueryString["model"], out param))
                model.FillModel(param);

            return PartialView("_ModelPrices", model);
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

                    param = context.SaveChanges();
                }
            }

            return Content("OK");
        }
    }
}
