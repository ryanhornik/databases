using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    [Authorize]
    public class BoxofficeController : Controller
    {
        theme_park_dbEntities db = new theme_park_dbEntities();
        // GET: Boxoffice
        public ActionResult Index()
        {
            ViewBag.Success = "";
            ViewBag.Failure = "";
            ViewBag.Types = db.ticket_types.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? ticketId, int? ticketTypeId)
        {
            ViewBag.Success = "";
            ViewBag.Failure = "";
            ViewBag.Types = db.ticket_types.ToList();
            if (ticketId.HasValue)
            {
                var ticket = db.ticket_sales.Find(ticketId);
                if (ticket != null)
                {
                    if (ticket.redemption_date.HasValue)
                    {
                        ViewBag.Failure = "Ticket # " + ticketId + " was already redeemed on " + ticket.redemption_date.Value.ToLongDateString();
                    }
                    else
                    {
                        ticket.redemption_date = DateTime.Today;
                        db.SaveChanges();
                        ViewBag.Success = "You have successfully redeemed ticket # " + ticketId;
                    }
                }
                else
                {
                    ViewBag.Failure = ticketId + " is not a valid ticket ID";
                }
            }
            else if (ticketTypeId.HasValue)
            {
                if (db.ticket_types.Any(s => s.ticket_type_id == ticketTypeId.Value))
                {
                    var ticket = db.CreateTicket(ticketTypeId.Value);
                    ticket.sale_location = "Box Office";
                    db.ticket_sales.Add(ticket);
                    db.SaveChanges();

                    return RedirectToAction("Ticket");
                }
                else
                {
                    ViewBag.Failure = ticketTypeId + " is not a valid ticket type";
                }
            }
            return View();
        }

        public ActionResult Ticket()
        {
            var ticket = db.ticket_sales.OrderByDescending(s => s.ticket_id).First();
            ViewBag.MS = ticket.GeneratePDF();
            return View();
        }
    }
}