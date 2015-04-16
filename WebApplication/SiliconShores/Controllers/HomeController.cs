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
            var restaurantReports = new List<restaurant_daily_reports>();

            Random r = new Random();

            for(var date = startDate; DateTime.Compare(date, stopDate) < 0; date = date.AddDays(1))
            {
                var numPatrons = (double) db.ticket_sales.Count(s => s.redemption_date != null && DateTime.Compare(s.redemption_date.Value, date) == 0);
                numPatrons *= 1.25;

                foreach (var restaurant in db.restaurants)
                {
                    double served = (numPatrons/6);
                    served += served*(2*r.NextDouble() - 1)*.25;
                    double plateCost = 10 * r.NextDouble() + 7.5;
                    decimal grossIncome = (decimal) (plateCost*served);
                    restaurantReports.Add(new restaurant_daily_reports()
                    {
                        restaurant = restaurant,
                        report_date = date,
                        gross_income = grossIncome,
                        patrons_served = (int) Math.Ceiling(served)
                    });
                }
            }

            db.restaurant_daily_reports.AddRange(restaurantReports);
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