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
    
    public partial class SearchRequest
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public System.DateTime SearchDate { get; set; }
        public string Request { get; set; }
        public System.Data.Entity.Spatial.DbGeography Position { get; set; }
        public string UniqueDeviceIdentifier { get; set; }
    
        public virtual User User { get; set; }
    }
}
