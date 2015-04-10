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
    
    public partial class hotel_reservations
    {
        [Required]
        [Display(Name = "Reservation ID")]
        public int reservation_id { get; set; }
        [Required]
        [Display(Name = "Hotel ID")]
        public int hotel_id { get; set; }
        [Required]
        [Display(Name = "Room Number")]
        public int room_number { get; set; }
        [Required]
        [Display(Name = "Reservation Check-In Date")]
        public System.DateTime reservation_checkin_date { get; set; }
        [Required]
        [Display(Name = "Reservation Check-Out Date")]
        public System.DateTime reservation_checkout_date { get; set; }
        [Required]
        [Display(Name = "Total Reservation Cost")]
        public decimal total_reservation_cost { get; set; }
        [Required]
        [Display(Name = "Paid In Full")]
        public bool paid_in_full { get; set; }
        [Required]
        [Display(Name = "Hotel Rooms")]
        public virtual hotel_rooms hotel_rooms { get; set; }
    }
}
