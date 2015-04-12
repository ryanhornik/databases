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
    
    public partial class theme_park
    {
        public theme_park()
        {
            this.daily_weather = new HashSet<daily_weather>();
            this.employees = new HashSet<employee>();
            this.theme_areas = new HashSet<theme_areas>();
            this.ticket_types = new HashSet<ticket_types>();
            this.ticket_sales = new HashSet<ticket_sales>();
        }
    
        public int theme_park_id { get; set; }
        public string theme_park_name { get; set; }
        public System.TimeSpan park_open { get; set; }
        public System.TimeSpan park_close { get; set; }
        public string park_country { get; set; }
        public string park_state_or_province { get; set; }
        public string park_city { get; set; }
        public string park_street_address { get; set; }
    
        public virtual ICollection<daily_weather> daily_weather { get; set; }
        public virtual ICollection<employee> employees { get; set; }
        public virtual ICollection<theme_areas> theme_areas { get; set; }
        public virtual ICollection<ticket_sales> ticket_sales { get; set; }
        public virtual ICollection<ticket_types> ticket_types { get; set; }
    }
}
