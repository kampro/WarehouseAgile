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
        public int selectedMakeId;
        private List<Make> makes;
        private List<Model> models;

        public List<Make> MakesList
        {
            get { return this.makes; }
        }

        public List<Model> ModelsList
        {
            get { return this.models; }
        }

        public OfferModel()
        {
            this.FillMakesList();
            this.selectedMakeId = 0;
        }

        // Fills models lists with make specified data
        public void FillModelsList(int makeId)
        {
            this.selectedMakeId = makeId;
            this.models = new List<Model>();

            using (AppDBEntities context = new AppDBEntities())
            {
                var models = from m in context.Models
                             where m.Id_make == makeId
                             select m;
                foreach (Model m in models)
                    this.models.Add(m);
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
    }
}