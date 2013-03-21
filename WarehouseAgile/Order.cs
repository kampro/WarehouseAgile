//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WarehouseAgile
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public int Id_seller { get; set; }
        public int Id_car { get; set; }
        public int Id_color { get; set; }
        public int Id_state { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<System.DateTime> Realization { get; set; }
        public Nullable<int> Id_customer { get; set; }
    
        public virtual Color Color { get; set; }
        public virtual EquipmentPrice EquipmentPrice { get; set; }
        public virtual State State { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
