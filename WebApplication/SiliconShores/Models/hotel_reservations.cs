
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
    
public partial class hotel_reservations
{

    public int reservation_id { get; set; }

    public int hotel_id { get; set; }

    public int room_number { get; set; }

    public System.DateTime reservation_checkin_date { get; set; }

    public System.DateTime reservation_checkout_date { get; set; }

    public decimal total_reservation_cost { get; set; }

    public bool paid_in_full { get; set; }



    public virtual hotel_rooms hotel_rooms { get; set; }

}

}
