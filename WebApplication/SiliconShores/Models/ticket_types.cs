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
    
    public partial class ticket_types
    {
        public ticket_types()
        {
            this.ticket_sales = new HashSet<ticket_sales>();
        }
    
        public int ticket_type_id { get; set; }
        public string ticket_name { get; set; }
        public string ticket_restrictions { get; set; }
        public Nullable<decimal> ticket_price { get; set; }
        public Nullable<int> theme_park_id { get; set; }
    
        public virtual theme_park theme_park { get; set; }
        public virtual ICollection<ticket_sales> ticket_sales { get; set; }
    }
}
