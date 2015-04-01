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
        [Display(Name = "Ticket Types")]
        public ticket_types TicketType { get; set; }

        [Required]
        [Display(Name = "Number of Tickets")]
        public int NumberOfTickets { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public room_types RoomType { get; set; }

    }
}