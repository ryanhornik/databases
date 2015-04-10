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
        [Required]
        [Display(Name = "Attractions ID")]
        public int attractions_id { get; set; }
        [Required]
        [Display(Name = "Attraction Name")]
        public string attraction_name { get; set; }
        [Required]
        [Display(Name = "Theme Area ID")]
        public int theme_area_id { get; set; }
        [Required]
        [Display(Name = "Attraction Description")]
        public string attraction_description { get; set; }
        [Required]
        [Display(Name = "Picture Path")]
        public string picture_path { get; set; }
        [Required]
        [Display(Name = "Is Working?")]
        public bool is_working { get; set; }
        [Required]
        [Display(Name = "Date Opened")]
        public System.DateTime date_opened { get; set; }
        [Required]
        [Display(Name = "Breakdowns")]
        public virtual ICollection<breakdown> breakdowns { get; set; }
        [Required]
        [Display(Name = "Daily Ride Report")]
        public virtual ICollection<daily_ride_report> daily_ride_report { get; set; }
        [Required]
        [Display(Name = "Theme Areas")]
        public virtual theme_areas theme_areas { get; set; }

        public String getImagePath()
        {
            return "/Content/Images" + theme_areas.theme_area_pictures + picture_path;
        }
    }
}
