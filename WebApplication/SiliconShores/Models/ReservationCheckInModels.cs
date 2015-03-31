using System.ComponentModel.DataAnnotations;

namespace SiliconShores.Models
{
    public class ReservationCheckInModels
    {
        [Required]
        [Display(Name = "Arrival Date")]
        public string arrivalDate { get; set; }

        [Required]
        [Display(Name = "Nights")]
        public int nights { get; set; }

        [Required]
        [Display(Name = "Adults")]
        public int adults { get; set; }

        
    }
}