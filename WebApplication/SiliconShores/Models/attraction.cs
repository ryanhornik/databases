//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiliconShores.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class attraction
    {
        public attraction()
        {
            this.breakdowns = new HashSet<breakdown>();
            this.daily_ride_report = new HashSet<daily_ride_report>();
        }
        public int attractions_id { get; set; }
        public string attraction_name { get; set; }
        public int theme_area_id { get; set; }
        public string attraction_description { get; set; }
        public string picture_path { get; set; }
        public bool is_working { get; set; }
        public System.DateTime date_opened { get; set; }
        public virtual ICollection<breakdown> breakdowns { get; set; }
        public virtual ICollection<daily_ride_report> daily_ride_report { get; set; }
        public virtual theme_areas theme_areas { get; set; }
    }
}
