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
    
    public partial class Seller
    {
        public int Id { get; set; }
        public Nullable<System.Guid> Id_user { get; set; }
        public Nullable<int> Id_branch { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    
        public virtual Branch Branch { get; set; }
    }
}
