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
            OfferModel model = new OfferModel();
            
            int param;

            if(int.TryParse(Request.QueryString["make"], out param))
                model.FillModelsList(param);

            if (int.TryParse(Request.QueryString["model"], out param))
                model.FillEquipmentList(param);

            return View(model);
        }
    }
}
