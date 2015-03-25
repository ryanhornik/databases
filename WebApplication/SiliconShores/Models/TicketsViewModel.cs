using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SiliconShores.Models
{
    public class TicketsViewModel
    {
        [Display(Name = "Number of Childrens Tickets")]
        public int ChildrenTickets { get; set; }

        [Display(Name = "Number of Adult Tickets")]
        public int AdultTickets { get; set; }

        [Display(Name = "Number of Senior Tickets")]
        public int SeniorTickets { get; set; }

        [Display(Name = "Number of Military Tickets")]
        public int MilitaryTickets { get; set; }
    }
}