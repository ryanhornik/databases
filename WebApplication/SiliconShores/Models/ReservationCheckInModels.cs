using System.ComponentModel.DataAnnotations;

namespace SiliconShores.Models
{
    public class ReservationCheckInModels
    {
        [Required]
        [Display(Name = "Arrival Date")]
        public string ArrivalDate { get; set; }

        [Required]
        [Display(Name = "Nights")]
        public int Nights { get; set; }

        [Required]
        [Display(Name = "Adults")]
        public int Adults { get; set; }

        
    }
}