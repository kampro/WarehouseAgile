using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using WarehouseAgile;

namespace WarehouseAgile.Models
{
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
            this.makes = new List<Make>();

            using (AppDBEntities context = new AppDBEntities())
            {
                //DbSet<Make> ds = context.Makes;
                this.makes = context.Makes.ToList();
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
            this.models = new List<Model>();

            using (AppDBEntities context = new AppDBEntities())
            {
                this.models = (from m in context.Models
                               where m.Id_make == makeId
                               select m).ToList();
            }
        }

        #endregion
    }

    public class ModelPricesModel
    {
        #region Nested types

        public class EquipmentPrice
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public float Price { get; set; }
        }

        #endregion

        #region Fields

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
            this.equipmentPrices = new List<EquipmentPrice>();

            using (AppDBEntities context = new AppDBEntities())
            {
                this.currentModel = (from m in context.Models
                                     where m.Id == modelId
                                     select m).FirstOrDefault();

                this.equipmentPrices = (from ep in context.EquipmentPrices
                                        join e in context.Equipments on ep.Id_equipment equals e.Id
                                        where ep.Id_model == modelId
                                        select new EquipmentPrice { Id = ep.Id, Name = e.Name, Price = ep.Price }).ToList();
            }
        }

        #endregion
    }
}