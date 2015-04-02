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
        public ActionResult Index()
        {
            return View(db.ticket_types.ToList());
        }

        public ActionResult Buy() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(int childrenTickets = 0, int adultTickets = 0, int seniorTickets = 0, int militaryTickets = 0)
        {
            var theme_park = db.theme_park.First(s => s.theme_park_name.Equals("Silicon Shores"));
            var ticket_types = db.ticket_types;
            var sale_date = DateTime.Today;

            
            if (ModelState.IsValid)
            {
                ticket_sales sale = new ticket_sales();
                sale.sale_date = sale_date;
                sale.redemption_date = null;
                sale.theme_park = theme_park;
                sale.sale_location = "Online";

                List<ticket_sales> fullSale = new List<ticket_sales>();

                sale.ticket_types = ticket_types.First(s => s.ticket_name.Equals("Child"));
                fullSale.AddRange(Enumerable.Repeat(sale, childrenTickets));

                sale.ticket_types = ticket_types.First(s => s.ticket_name.Equals("Adult"));
                fullSale.AddRange(Enumerable.Repeat(sale, adultTickets));

                sale.ticket_types = ticket_types.First(s => s.ticket_name.Equals("Senior"));
                fullSale.AddRange(Enumerable.Repeat(sale, seniorTickets));
                
                sale.ticket_types = ticket_types.First(s => s.ticket_name.Equals("Military/Veteran"));
                fullSale.AddRange(Enumerable.Repeat(sale, militaryTickets));

                foreach (var ticket in fullSale)
                {
                    db.ticket_sales.Add(ticket);
                    db.SaveChanges();
                }
                
            }

            return RedirectToAction("Index");
        }

    }
}