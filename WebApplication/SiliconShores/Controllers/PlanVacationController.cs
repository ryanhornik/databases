using SiliconShores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Spire.Barcode;
using System.Drawing;
using System.Net.Mail;

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
            ViewBag.Rooms = new SelectList(db.hotel_rooms, "hotel_and_room_type", "room_number");
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        public ActionResult ConfirmPurchase(DateTime arrivalDate, int nights, IDictionary<int, int> ticketPurchase, int Hotels, int RoomTypes, int Room)
        {
            ViewBag.RoomType = db.room_types.Find(RoomTypes);
            ViewBag.Hotel = db.hotels.Find(Hotels);
            ViewBag.Room = db.hotel_rooms.First(s => s.hotel_id == Hotels && s.room_number == Room);
            ViewBag.Checkin = arrivalDate;
            ViewBag.Checkout = arrivalDate.AddDays(nights);
            ViewBag.Nights = nights;

            ViewBag.TicketPurchase = ticketPurchase.ToDictionary(d => db.ticket_types.Find(d.Key) , d => d.Value*(nights + 1) );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPurchase(DateTime arrivalDate, int nights, IDictionary<int, int> ticketPurchase, int Hotels, int RoomTypes, int Room, bool? post, string email)
        {

            List<ticket_sales> Sales = new List<ticket_sales>();
            List<int> ticketID = new List<int>();

            ticketPurchase = ticketPurchase.ToDictionary(s => s.Key, s => (nights+1)*s.Value);

            var selectedRoom = db.hotel_rooms.First(s => s.hotel_id == Hotels && s.room_number == Room);
            
            var hotelReservation = new hotel_reservations
            {
                hotel_rooms = selectedRoom,
                total_reservation_cost = selectedRoom.room_rate * nights,
                paid_in_full = false,
                reservation_checkin_date = arrivalDate,
                reservation_checkout_date = arrivalDate.AddDays(nights)
            };
            if (ModelState.IsValid)
            {
                db.hotel_reservations.Add(hotelReservation);
                db.SaveChanges();
            }

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

            Sales = db.ticket_sales.OrderByDescending(t => t.ticket_id).Take(totalSales.Count()).ToList();
            foreach (ticket_sales sales in Sales)
            {
                ticketID.Add(sales.ticket_id);
            }

            MailMessage mail = new MailMessage("siliconshoressmtp@gmail.com", email)
            {
                Subject = "PURCHASE CONFIRMATION",

                Attachments = { new Attachment("~/App_Data/eTicketsForVacation.pdf") }
            };
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);

            return RedirectToAction("Index", "Home");
        }

        public JsonResult RoomSelection(string selectedHotel, string selectedRoomType)
        {

            if (selectedHotel.IsEmpty() || selectedRoomType.IsEmpty())
                return null;

            int sh = Convert.ToInt32(selectedHotel);
            int srt = Convert.ToInt32(selectedRoomType);

            var rooms = db.hotel_rooms.Where(s =>
                s.hotel_id == sh &&
                s.room_type_id == srt);

            return Json(rooms.Select(s => new
            {
                hotel_id = s.hotel_id,
                room_number = s.room_number,
                room_rate = s.room_rate,
                room_type_id = s.room_type_id
            }
                ), JsonRequestBehavior.AllowGet);
        }

    }
}