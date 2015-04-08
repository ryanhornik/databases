using SiliconShores.Models;
using System.Data.Entity;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiliconShores.Controllers
{
    public class PlanVacationController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: PlanVacation
        public ActionResult Index()
        {
            ViewBag.RoomTypes = new SelectList(db.room_types, "room_type_id", "room_types_string");
            ViewBag.Hotels = new SelectList(db.hotels, "hotel_id", "hotel_name");
            ViewBag.Rooms = new SelectList(db.hotel_rooms);
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime arrivalDate, int nights, IDictionary<int, int> ticketPurchase)
        {
            ticketPurchase = ticketPurchase.ToDictionary(s => s.Key, s => (nights+1)*s.Value);
            List<int> totalSales = new List<int>();
            
            foreach (var ticket in ticketPurchase)
            {
                totalSales.AddRange(Enumerable.Repeat(ticket.Key,ticket.Value));
            }
            foreach (var s in totalSales)
            {
                db.ticket_sales.Add(db.CreateTicket(s));
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckNow(DateTime arrivalDate, int nights, int adults, int children, int infants, room_types roomtype, int rooms)
        {
            var theme_park = db.theme_park.First(s => s.theme_park_name.Equals("Silicon Shores"));
            var ticket_types = db.ticket_types;
            
            
            if(ModelState.IsValid)
            {
                ticket_sales sale = new ticket_sales();
                sale.sale_date = arrivalDate;
                sale.redemption_date = null;
                sale.theme_park = theme_park;
                sale.sale_location = "Online";

                List<ticket_sales> fullSale = new List<ticket_sales>();

                if(nights > 0)
                {
                    nights++;
                    adults = nights * adults;
                    children = nights * children;
                }

                hotel_reservations reservation = new hotel_reservations();
                reservation.reservation_checkin_date = arrivalDate;
                TimeSpan duration = new TimeSpan(nights,0,0,0);
                DateTime endDate = arrivalDate.Add(duration);
                reservation.reservation_checkout_date = endDate;
                reservation.reservation_id = 3;
                reservation.hotel_id = 2;
                reservation.room_number = 3;

                decimal price = 102.99m;
                reservation.total_reservation_cost = price;
                reservation.paid_in_full = true;



                sale.ticket_types = ticket_types.First(s => s.ticket_name.Equals("Adult"));
                fullSale.AddRange(Enumerable.Repeat(sale, adults));

                sale.ticket_types = ticket_types.First(s => s.ticket_name.Equals("Child"));
                fullSale.AddRange(Enumerable.Repeat(sale, children));


                foreach (var ticket in fullSale)
                {
                    db.ticket_sales.Add(ticket);
                    db.SaveChanges();
                }

                db.hotel_reservations.Add(reservation);
                db.SaveChanges();
            }

            return RedirectToAction("Index","Home");
        }


    }
}