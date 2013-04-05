﻿using System;
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

        #region Makes

        //
        // GET: /Offer/GetMakes

        public PartialViewResult GetMakes()
        {
            return PartialView("_MakesSelect");
        }

        //
        // GET: /Offer/GetMakeDetails

        //[ChildActionOnly] - the method is used with AJAX, it can't be ChildAction
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
                        return Content(string.Format("<h3>Edycja {1}</h3>" +
                            "<div class=\"row-clean\"><a href=\"{2}\" rel=\"delete-make\" class=\"ui-state-default ui-corner-all icon-anchor\"><span class=\"ui-icon ui-icon-trash\"></span></a></div>" +
                            "<input type=\"hidden\" name=\"make-id\" value=\"{0}\">" +
                            "<div class=\"row-clean\">Nazwa: <input type=\"text\" name=\"make\" value=\"{1}\" /></div>" +
                            "<div class=\"row-clean\"><input type=\"submit\" value=\"Zapisz\" /></div>", make.Id, make.Name, Url.Action("DeleteMake", "Offer", new { make = param })));
                }
            }

            return Content("<h3>Edycja</h3><div class=\"row-clean\">Brak rekordu o wskazanym identyfikatorze</div>");
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
                    string make = Request.Form["make"];

                    if ((from m in context.Makes where m.Name == make select m).Any())
                        //throw new ApplicationException("Make already exists");
                        make = "aa";
                    else
                    {
                        context.Makes.Add(new Make() { Name = make });
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

        //
        // GET: /Offer/DeleteMake

        public ActionResult DeleteMake()
        {
            int param;

            if (int.TryParse(Request.QueryString["make"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Make make = (from m in context.Makes
                                 where m.Id == param
                                 select m).First();

                    context.Makes.Remove(make);
                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        #endregion

        #region Models

        //
        // GET: /Offer/GetModels

        public PartialViewResult GetModels(int make)
        {
            ModelsModel model = new ModelsModel();
            int param;

            if (!int.TryParse(Request.QueryString["make"], out param))
                param = make;

            model.FillModelsList(param);

            return PartialView("_ModelsSelect", model);
        }

        //
        // GET: /Offer/GetModelDetails

        public PartialViewResult GetModelDetails()
        {
            ModelDetailsModel model = new ModelDetailsModel();
            int param;

            if (int.TryParse(Request.QueryString["model"], out param))
                model.FillModel(param);

            return PartialView("_ModelDetails", model);
        }

        //
        // POST: /Offer/AddModelEquipment

        [HttpPost]
        public ActionResult AddModelEquipment()
        {
            int param;
            int param2;

            if (int.TryParse(Request.Form["model-id"], out param) && int.TryParse(Request.Form["equipment"], out param2))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    if ((from ep in context.EquipmentPrices
                         where ep.Id_model == param && ep.Id_equipment == param2
                         select ep).Any())
                        throw new ApplicationException("Configuration already exists");
                    else
                    {
                        context.EquipmentPrices.Add(new EquipmentPrice() { Id_model = param, Id_equipment = param2, Price = 0f });
                        context.SaveChanges();
                    }
                }
            }

            return Content("OK");
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
        // GET: /Offer/DeleteEquipmentModel

        public ActionResult DeleteEquipmentModel()
        {
            int param;

            if (int.TryParse(Request.QueryString["equipment"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    EquipmentPrice equipmentPrice = (from ep in context.EquipmentPrices
                                                     where ep.Id == param
                                                     select ep).First();

                    context.EquipmentPrices.Remove(equipmentPrice);
                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        //
        // GET: /Offer/DeleteModel

        public ActionResult DeleteModel()
        {
            int param;

            if (int.TryParse(Request.QueryString["model"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Model model = (from m in context.Models
                                   where m.Id == param
                                   select m).First();

                    context.Models.Remove(model);
                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        #endregion

        #region Equipments

        //
        // GET: /Offer/GetEquipments

        public PartialViewResult GetEquipments()
        {
            return PartialView("_EquipmentsSelect");
        }

        //
        // GET: /Offer/GetEquipmentDetails

        public ActionResult GetEquipmentDetails()
        {
            int param;

            if (int.TryParse(Request.QueryString["equipment"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Equipment equipment = (from e in context.Equipments
                                           where e.Id == param
                                           select e).FirstOrDefault();

                    if (equipment != null)
                        return Content(string.Format("<h3>Edycja {1}</h3>" +
                            "<div class=\"row-clean\"><a href=\"{2}\" rel=\"delete-equipment\" class=\"ui-state-default ui-corner-all icon-anchor\"><span class=\"ui-icon ui-icon-trash\"></span></a></div>" +
                            "<input type=\"hidden\" name=\"equipment-id\" value=\"{0}\">" +
                            "<div class=\"row-clean\">Nazwa: <input type=\"text\" name=\"equipment\" value=\"{1}\" /></div>" +
                            "<div class=\"row-clean\"><input type=\"submit\" value=\"Zapisz\" /></div>", equipment.Id, equipment.Name, Url.Action("DeleteEquipment", "Offer", new { equipment = param })));
                }
            }

            return Content("<h3>Edycja</h3><div class=\"row-clean\">Brak rekordu o wskazanym identyfikatorze</div>");
        }

        //
        // POST: /Offer/AddEquipment

        [HttpPost]
        public ActionResult AddEquipment()
        {
            if (Request.Form["equipment"].Length > 0)
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    string equipment = Request.Form["equipment"];

                    if ((from e in context.Equipments where e.Name == equipment select e).Any())
                        throw new ApplicationException("Equipment already exists");
                    else
                    {
                        context.Equipments.Add(new Equipment() { Name = equipment });
                        context.SaveChanges();
                    }
                }
            }

            return Content("OK");
        }

        //
        // POST: /Offer/SaveEquipment

        [HttpPost]
        public ActionResult SaveEquipment()
        {
            int param;

            if (int.TryParse(Request.Form["equipment-id"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Equipment equipment = (from e in context.Equipments
                                           where e.Id == param
                                           select e).First();

                    equipment.Name = Request.Form["equipment"];

                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        //
        // GET: /Offer/DeleteEquipment

        public ActionResult DeleteEquipment()
        {
            int param;

            if (int.TryParse(Request.QueryString["equipment"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Equipment equipment = (from e in context.Equipments
                                           where e.Id == param
                                           select e).First();

                    context.Equipments.Remove(equipment);
                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        #endregion

        #region Colors

        //
        // GET: /Offer/GetColors

        public PartialViewResult GetColors()
        {
            return PartialView("_ColorsSelect");
        }

        //
        // GET: /Offer/GetColorDetails

        public ActionResult GetColorDetails()
        {
            int param;

            if (int.TryParse(Request.QueryString["color"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Color color = (from c in context.Colors
                                   where c.Id == param
                                   select c).FirstOrDefault();

                    if (color != null)
                        return Content(string.Format("<h3>Edycja {1}</h3>" +
                            "<div class=\"row-clean\"><a href=\"{2}\" rel=\"delete-color\" class=\"ui-state-default ui-corner-all icon-anchor\"><span class=\"ui-icon ui-icon-trash\"></span></a></div>" +
                            "<input type=\"hidden\" name=\"color-id\" value=\"{0}\">" +
                            "<div class=\"row-clean\">Nazwa: <input type=\"text\" name=\"color\" value=\"{1}\" /></div>" +
                            "<div class=\"row-clean\">Cena: <input type=\"text\" name=\"price\" value=\"{3}\" /> zł</div>" +
                            "<div class=\"row-clean\"><input type=\"submit\" value=\"Zapisz\" /></div>", color.Id, color.Name, Url.Action("DeleteColor", "Offer", new { color = param }), color.Price));
                }
            }

            return Content("<h3>Edycja</h3><div class=\"row-clean\">Brak rekordu o wskazanym identyfikatorze</div>");
        }

        //
        // POST: /Offer/AddColor

        [HttpPost]
        public ActionResult AddColor()
        {
            if (Request.Form["color"].Length > 0)
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    string color = Request.Form["color"];

                    if ((from c in context.Colors where c.Name == color select c).Any())
                        throw new ApplicationException("Color already exists");
                    else
                    {
                        float price;

                        if (!float.TryParse(Request.Form["price"], out price))
                            price = 0f;

                        context.Colors.Add(new Color() { Name = color, Price = price });
                        context.SaveChanges();
                    }
                }
            }

            return Content("OK");
        }

        //
        // POST: /Offer/SaveColor

        [HttpPost]
        public ActionResult SaveColor()
        {
            int param;

            if (int.TryParse(Request.Form["color-id"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Color color = (from c in context.Colors
                                   where c.Id == param
                                   select c).First();

                    color.Name = Request.Form["color"];
                    color.Price = float.Parse(Request.Form["price"]);

                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        //
        // GET: /Offer/DeleteColor

        public ActionResult DeleteColor()
        {
            int param;

            if (int.TryParse(Request.QueryString["color"], out param))
            {
                using (AppDBEntities context = new AppDBEntities())
                {
                    Color color = (from c in context.Colors
                                   where c.Id == param
                                   select c).First();

                    context.Colors.Remove(color);
                    context.SaveChanges();
                }
            }

            return Content("OK");
        }

        #endregion
    }
}
