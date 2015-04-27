using SiliconShores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Mail;
using System.Drawing;
using System.IO;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPurchase(IDictionary<int, int> ticketPurchase, string email)
        {

            List<int> totalSales = new List<int>();

            foreach (var ticket in ticketPurchase)
            {
                totalSales.AddRange(Enumerable.Repeat(ticket.Key, ticket.Value));
            }

            var fullPurchase = totalSales.Select(s => db.CreateTicket(s));
            db.ticket_sales.AddRange(fullPurchase);
            db.SaveChanges();

            var allTickets = db.ticket_sales
                .OrderByDescending(t => t.ticket_id)
                .Take(totalSales.Count());
            var pdfs = new List<MemoryStream>();
            foreach (var ticket in allTickets)
            {
                pdfs.Add(ticket.GeneratePDF());
            }

            
            MailMessage mail = new MailMessage("siliconshoressmtp@gmail.com", email)
            {
                Subject = "PURCHASE CONFIRMATION",
            };
            var count = new Random().Next(500);
            foreach (var pdf in pdfs)
            {
                mail.Attachments.Add(new Attachment(pdf, "Ticket"+count.ToString("X")+".pdf", "application/pdf"));
                count+=3;
            }

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);


            return RedirectToAction("Index", "Home");
        }
    }
}