using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using WarehouseAgile;

namespace WarehouseAgile.Models
{
    public class OfferModel
    {
        #region Fields

        public int selectedMakeId;
        public int selectedModelId;
        private List<Make> makes;
        private List<Model> models;
        private List<EquipmentPrice> equipmentPrices;

        #endregion

        #region Properties

        public List<Make> MakesList
        {
            get { return this.makes; }
        }

        public List<Model> ModelsList
        {
            get { return this.models; }
        }

        public List<EquipmentPrice> EquipmentPricesList
        {
            get { return this.equipmentPrices; }
        }

        #endregion

        #region Methods

        public OfferModel()
        {
            this.FillMakesList();
            this.selectedMakeId = 0;
            this.selectedModelId = 0;
        }

        // Fills models lists with make specified data
        public void FillModelsList(int makeId)
        {
            this.selectedMakeId = makeId;
            this.models = new List<Model>();

            using (AppDBEntities context = new AppDBEntities())
            {
                var modelsDB = from m in context.Models
                               where m.Id_make == makeId
                               select m;
                foreach (Model m in modelsDB)
                    this.models.Add(m);
            }
        }

        // Fills equipments for specified model
        public void FillEquipmentList(int modelId)
        {
            this.selectedModelId = modelId;
            this.equipmentPrices = new List<EquipmentPrice>();

            using (AppDBEntities context = new AppDBEntities())
            {
                var equipmentPricesDB = from ep in context.EquipmentPrices
                                        where ep.Id_model == modelId
                                        select ep;
                foreach (EquipmentPrice ep in equipmentPricesDB)
                    this.equipmentPrices.Add(ep);
            }
        }

        private void FillMakesList()
        {
            this.makes = new List<Make>();

            using (AppDBEntities context = new AppDBEntities())
            {
                //DbSet<Make> ds = context.Makes;
                foreach (Make m in context.Makes)
                    this.makes.Add(m);
            }
        }

        #endregion
    }
}