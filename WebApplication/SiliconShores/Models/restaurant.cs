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
    
    public partial class restaurant
    {
        public restaurant()
        {
            this.restaurant_daily_reports = new HashSet<restaurant_daily_reports>();
        }
    
        public int restaurant_id { get; set; }
        public string restaurant_name { get; set; }
        public int food_category_id { get; set; }
        public int theme_area_id { get; set; }
    
        public virtual food_categories food_categories { get; set; }
        public virtual ICollection<restaurant_daily_reports> restaurant_daily_reports { get; set; }
        public virtual theme_areas theme_areas { get; set; }
    }
}
