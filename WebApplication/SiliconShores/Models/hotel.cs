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
    
    public partial class hotel
    {
        public hotel()
        {
            this.hotel_rooms = new HashSet<hotel_rooms>();
        }
    
        public int hotel_id { get; set; }
        public string hotel_name { get; set; }
        public bool pets_allowed { get; set; }
        public int theme_area_id { get; set; }
    
        public virtual ICollection<hotel_rooms> hotel_rooms { get; set; }
        public virtual theme_areas theme_areas { get; set; }
    }
}
