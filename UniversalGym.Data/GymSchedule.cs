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
    
    public partial class GymSchedule
    {
        public int GymScheduleId { get; set; }
        public int GymId { get; set; }
        public int TypeWeekDayId { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
    
        public virtual Gym Gym { get; set; }
        public virtual TypeWeekDay TypeWeekDay { get; set; }
    }
}
