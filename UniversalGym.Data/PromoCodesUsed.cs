//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniversalGym.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PromoCodesUsed
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> PromoCodeId { get; set; }
        public Nullable<System.DateTime> DateUsed { get; set; }
    
        public virtual PromoCode PromoCode { get; set; }
        public virtual User User { get; set; }
    }
}
