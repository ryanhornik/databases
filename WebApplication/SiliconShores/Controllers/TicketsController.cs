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

         //GET: Tickets
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

            if (ModelState.IsValid)
            {
                for (int i = 0; i < ChildrenTickets; i++)
                {
                    ticket_sales sale = new ticket_sales();
                    sale.ticket_type_id = 1;
                    sale.redemption_date = DateTime.Today;
                    sale.redemption_date = null;
                    sale.theme_park_id = 2;
                    sale.sale_location = "Online";
                    db.ticket_sales.Add(sale);
                    db.SaveChanges();
                }

                for (int i = 0; i < AdultTickets; i++)
                {
                    ticket_sales sale = new ticket_sales();
                    sale.ticket_type_id = 2;
                    sale.redemption_date = DateTime.Today;
                    sale.redemption_date = null;
                    sale.theme_park_id = 2;
                    sale.sale_location = "Online";
                    db.ticket_sales.Add(sale);
                    db.SaveChanges();
                }

                for (int i = 0; i < SeniorTickets; i++)
                {
                    ticket_sales sale = new ticket_sales();
                    sale.ticket_type_id = 3;
                    sale.redemption_date = DateTime.Today;
                    sale.redemption_date = null;
                    sale.theme_park_id = 2;
                    sale.sale_location = "Online";
                    db.ticket_sales.Add(sale);
                    db.SaveChanges();
                }

                for (int i = 0; i < MilitaryTickets; i++)
                {
                    ticket_sales sale = new ticket_sales();
                    sale.ticket_type_id = 4;
                    sale.redemption_date = DateTime.Today;
                    sale.redemption_date = null;
                    sale.theme_park_id = 2;
                    sale.sale_location = "Online";
                    db.ticket_sales.Add(sale);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("/Home/Index");
        }

    }
}