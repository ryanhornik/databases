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
    
    public partial class theme_areas
    {
        public theme_areas()
        {
            this.attractions = new HashSet<attraction>();
            this.hotels = new HashSet<hotel>();
            this.restaurants = new HashSet<restaurant>();
        }
    
        public int theme_area_id { get; set; }
        public string theme_area_name { get; set; }
        public string theme_area_description { get; set; }
        public string theme_area_pictures { get; set; }
        public int theme_park_id { get; set; }
    
        public virtual ICollection<attraction> attractions { get; set; }
        public virtual ICollection<hotel> hotels { get; set; }
        public virtual ICollection<restaurant> restaurants { get; set; }
        public virtual theme_park theme_park { get; set; }

        public List<String> getImagePaths()
        {
            List<String> images = new List<String>();
            foreach (attraction attr in attractions)
            {
                images.Add(attr.getImagePath());
            }
            return images;
        }
    
    }
}
