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
            Random r = new Random();
            var rideReports = new List<daily_ride_report>();
            var stopDate = new DateTime(2015, 4, 14);
            for (var date = stopDate.AddYears(-1); date < stopDate; date = date.AddDays(1))
            {
                int peopleInPark = db.ticket_sales.Count(s => s.redemption_date != null && (DateTime.Compare(s.redemption_date.Value ,date) == 0));
                foreach (var ride in db.attractions)
                {
                    rideReports.Add(new daily_ride_report()
                    {
                    attraction = ride,
                    ride_report_date = date,
                    total_riders = r.Next(peopleInPark/8, (int) (peopleInPark*1.5))
                    });
                }
            }
            db.daily_ride_report.AddRange(rideReports);
            db.SaveChanges();
            return View();
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