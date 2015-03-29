using SiliconShores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace SiliconShores.Controllers
{
    public class TicketsController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: Tickets
        public ActionResult TicketInformation()
        {
            return View(db.ticket_types.ToList());
        }

        public ActionResult TicketPurchase() 
        {
            return View();
        }

        public ActionResult ProcessTickets(int ChildrenTickets, int AdultTickets, int SeniorTickets, int MilitaryTickets) 
        {

            for (int i = 0; i < ChildrenTickets; i++) 
            {
               
            }

                return View();//placeholder
        }

    }
}