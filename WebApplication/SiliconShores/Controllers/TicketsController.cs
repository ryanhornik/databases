using SiliconShores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

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

        public ActionResult ConfirmPurchase() 
        {
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPurchase(IDictionary<int, int> ticketPurchase, string email)
        {
            MailMessage mail = new MailMessage("siliconshoressmtp@gmail.com", email)
            {
                Subject = "This is only a test",
                Body = "This is the body of that test",
                Attachments = {null/*TODO Add a .PDF of the tickets here*/}
            };
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);

            List<int> totalSales = new List<int>();

            foreach (var ticket in ticketPurchase)
            {
                totalSales.AddRange(Enumerable.Repeat(ticket.Key, ticket.Value));
            }
            foreach (var s in totalSales)
            {
                db.ticket_sales.Add(db.CreateTicket(s));
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}