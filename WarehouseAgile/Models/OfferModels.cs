using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using WarehouseAgile;
using System.ComponentModel.DataAnnotations;

namespace WarehouseAgile.Models
{
    public class OfferModel
    {
        public Make dummyMake;
        public Model dumyModel;
        public Equipment dummyEquipment;
        public Color dummyColor;
    }

    public class MakesModel
    {
        #region Fields

        private List<Make> makes;

        #endregion

        #region Properties

        public List<Make> MakesList
        {
            get { return this.makes; }
        }

        #endregion

        #region Methods

        public MakesModel()
        {
            this.FillMakesList();
        }

        private void FillMakesList()
        {
            using (AppDBEntities context = new AppDBEntities())
            {
                //DbSet<Make> ds = context.Makes;
                this.makes = context.Makes.OrderBy(x => x.Name).ToList();
            }
        }

        #endregion
    }

    public class ModelsModel
    {
        #region Fields

        private List<Model> models;

        #endregion

        #region Properties

        public List<Model> ModelsList
        {
            get { return this.models; }
        }

        #endregion

        #region Methods

        // Fills models list for specified make
        public void FillModelsList(int makeId)
        {
            using (AppDBEntities context = new AppDBEntities())
            {
                this.models = (from m in context.Models
                               where m.Id_make == makeId
                               select m).OrderBy(x => x.Name).ToList();
            }
        }

        #endregion
    }

    public class ModelDetailsModel
    {
        #region Nested types

        public class EquipmentPrice
        {
            public int Id { get; set; }
            public string Name { get; set; }
            [Range(0, float.MaxValue, ErrorMessage = "Wartość musi być liczbą nieujemną")]
            [Display(Name = "Cena")]
            public float Price { get; set; }
        }

        #endregion

        #region Fields

        public string MakeName;
        private Model currentModel;
        private List<EquipmentPrice> equipmentPrices;

        #endregion

        #region Properties

        public Model CurrentModel
        {
            get { return this.currentModel; }
        }

        public List<EquipmentPrice> EquipmentPricesList
        {
            get { return this.equipmentPrices; }
        }

        #endregion

        #region Methods

        // Fills info and equipments list with variants and prices for specified model
        public void FillModel(int modelId)
        {
            using (AppDBEntities context = new AppDBEntities())
            {
                this.currentModel = (from m in context.Models
                                     where m.Id == modelId
                                     select m).FirstOrDefault();

                this.equipmentPrices = (from ep in context.EquipmentPrices
                                        where ep.Id_model == modelId
                                        select new EquipmentPrice { Id = ep.Id, Name = ep.Equipment.Name, Price = ep.Price }).ToList();

                this.MakeName = this.CurrentModel.Make.Name;
            }
        }

        #endregion
    }

    public class EquipmentsModel
    {
        #region Fields

        private List<Equipment> equipments;

        #endregion

        #region Properties

        public List<Equipment> EquipmentsList
        {
            get { return this.equipments; }
        }

        #endregion

        #region Methods

        public EquipmentsModel()
        {
            this.FillEquipmentsList();
        }

        private void FillEquipmentsList()
        {
            using (AppDBEntities context = new AppDBEntities())
            {
                this.equipments = context.Equipments.OrderBy(x => x.Name).ToList();
            }
        }

        #endregion
    }

    public class ColorsModel
    {
        #region Fields

        private List<Color> colors;

        #endregion

        #region Properties

        public List<Color> ColorsList
        {
            get { return this.colors; }
        }

        #endregion

        #region Methods

        public ColorsModel()
        {
            this.FillColorsList();
        }

        private void FillColorsList()
        {
            using (AppDBEntities context = new AppDBEntities())
            {
                this.colors = context.Colors.OrderBy(x => x.Name).ToList(); ;
            }
        }

        #endregion
    }
}