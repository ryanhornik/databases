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
    
    public partial class room_types
    {
        public room_types()
        {
            this.hotel_rooms = new HashSet<hotel_rooms>();
        }
    
        public int room_type_id { get; set; }
        public string room_types_string { get; set; }
    
        public virtual ICollection<hotel_rooms> hotel_rooms { get; set; }
    }
}
