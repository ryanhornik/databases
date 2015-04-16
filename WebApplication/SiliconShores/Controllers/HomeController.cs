using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    public class HomeController : Controller
    {
        theme_park_dbEntities db = new theme_park_dbEntities();

        public ActionResult Index()
        {
            var startDate = new DateTime(2014, 4, 14);
            var stopDate = startDate.AddYears(1);
            var hotelReservations = new List<hotel_reservations>();
            var totalRooms = db.hotel_rooms.Count();
            var roomList = db.hotel_rooms.ToArray();
            var saleList = db.ticket_sales.ToList();
            Random r = new Random();
            foreach (var ticket in saleList)
            {
                if (ticket.redemption_date != null)
                {
                    var date = ticket.redemption_date.Value;
                    var multiNightChance = 0.0; //Chance of a second night given they stay the first night
                    var percentOverNight = 0.0;
                    switch (date.DayOfWeek)
                    {
                        case DayOfWeek.Friday:
                            multiNightChance = .5;
                            percentOverNight = .075;
                            break;
                        case DayOfWeek.Saturday:
                            multiNightChance = .1;
                            percentOverNight = .075;
                            break;
                        case DayOfWeek.Thursday:
                            multiNightChance = .75;
                            percentOverNight = .03;
                            break;
                        case DayOfWeek.Sunday:
                            multiNightChance = .05;
                            percentOverNight = .03;
                            break;
                        default:
                            multiNightChance = .05;
                            percentOverNight = .01;
                            break;
                    }
                    if (r.NextDouble() < percentOverNight)
                    {
                        var nights = multiNightChance > r.NextDouble() ? 2 : 1;
                        var checkout = date.AddDays(nights);
                        var room = roomList[r.Next(totalRooms)];
                        while (RoomOccupied(room, date, checkout))
                        {
                            room = roomList[r.Next(totalRooms)];
                        }
                        
                        hotelReservations.Add(new hotel_reservations()
                        {
                            hotel_rooms = room,
                            paid_in_full = true,
                            reservation_checkin_date = date,
                            reservation_checkout_date = checkout,
                            total_reservation_cost = nights * room.room_rate,
                        });
                        var breakpoint = "excuse";
                    }
                }
            }

            db.hotel_reservations.AddRange(hotelReservations);
            db.SaveChanges();
            return View();
        }

        private static bool RoomOccupied(hotel_rooms hr, DateTime arrival, DateTime checkout)
        {
            return hr.hotel_reservations.Any(s =>
                (DateTime.Compare(s.reservation_checkin_date, arrival) <= 0 && DateTime.Compare(s.reservation_checkout_date, arrival) > 0 )||
                (DateTime.Compare(arrival, s.reservation_checkin_date) <= 0 && DateTime.Compare(checkout, s.reservation_checkin_date) > 0));
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";

            return View();
        }
    }
}