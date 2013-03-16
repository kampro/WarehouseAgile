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
            MakesModel model = new MakesModel();

            return View(model);
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
    }
}
