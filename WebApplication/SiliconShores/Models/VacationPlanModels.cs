using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace SiliconShores.Models
{
    public class VacationPlanViewModel
    {
        [Required]
        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [Display(Name = "Nights")]
        public int Nights { get; set; }

        [Required]
        [Display(Name = "Adults")]
        public int Adults { get; set; }

        [Required]
        [Display(Name = "Children")]
        public int Children { get; set; }

        [Required]
        [Display(Name = "Infants")]
        public int Infants { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public room_types RoomType { get; set; }

        [Required]
        [Display(Name = "Rooms")]
        public int Rooms { get; set; }
    }
}