using SiliconShores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Spire.Barcode;

namespace SiliconShores.Controllers
{
    public class TicketsController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

         //GET: Tickets
        public ActionResult Index()
        {
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        public ActionResult ConfirmPurchase(IDictionary<int, int> ticketPurchase) 
        {
            ViewBag.TicketPurchase = ticketPurchase.ToDictionary(d => db.ticket_types.Find(d.Key), d => d.Value);
            return View();
        }

        public ActionResult ThankYou() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPurchase(IDictionary<int, int> ticketPurchase, string email)
        {
            var totalSales = new List<int>();

            foreach (var ticket in ticketPurchase)
            {
                totalSales.AddRange(Enumerable.Repeat(ticket.Key, ticket.Value));
            }

            var fullPurchase = totalSales.Select(s => db.CreateTicket(s));
            db.ticket_sales.AddRange(fullPurchase);
            db.SaveChanges();

            var thread = new Thread(() => db.sendLastTickets(email, totalSales.Count,
                "Thank you for your ticket purchase! Please enjoy your day in the park.\n\n - Silicon Shores\n"));
            thread.Start();

            return RedirectToAction("ThankYou", "Tickets");
        }
    }
}