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
    
    public partial class GymInvoice
    {
        public int Id { get; set; }
        public System.DateTime StartPeriodDate { get; set; }
        public System.DateTime EndPeriodDate { get; set; }
        public int AmountPaid { get; set; }
        public bool IsCollected { get; set; }
        public int GymId { get; set; }
    
        public virtual Gym Gym { get; set; }
    }
}
