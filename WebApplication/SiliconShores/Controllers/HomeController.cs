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
            var startTime = DateTime.Now;
            string[] options = {"sunny", "partly cloudy", "mostly cloudy", "overcast", "rainy"};
            var date = new DateTime(2014, 4, 14);
            Random r = new Random();
            for (var k = 0; k < 365; k++)
            {
                string weatherCondition = "";
                var RNGeesus = r.NextDouble();
                if (RNGeesus >= 0.0 && RNGeesus < .15)
                {
                    weatherCondition = options[4];
                }
                else if (RNGeesus < .3)
                {
                    weatherCondition = options[3];
                }
                else if (RNGeesus < .5)
                {
                    weatherCondition = options[2];
                }
                else if(RNGeesus < .7)
                {
                    weatherCondition = options[1];
                }
                else if (RNGeesus <= 1.0)
                {
                    weatherCondition = options[0];
                }
                var low_temp = r.Next(32,80);
                var high_temp = low_temp + r.Next(1, 20);
                daily_weather dw = new daily_weather
                {
                    high_temp = high_temp,
                    low_temp = low_temp,
                    theme_park = db.theme_park.First(s => s.theme_park_name == "Silicon Shores"),
                    weather_conditions = weatherCondition,
                    weather_date = date
                };
                db.daily_weather.Add(dw);
                db.SaveChanges();

                var numSales = 0;
                switch (weatherCondition)
                {
                    case "sunny":
                        numSales = r.Next(200);
                        break;
                    case "partly cloudy":
                        numSales = r.Next(180);
                        break;
                    case "mostly cloudy":
                        numSales = r.Next(125);
                        break;
                    case "overcast":
                        numSales = r.Next(50);
                        break;
                    case "rainy":
                        numSales = r.Next(10);
                        break;
                }
                
                for (var i = 0; i < numSales; i++)
                {
                    var loc = r.Next(2);
                    
                    var sale = new ticket_sales
                    {
                        sale_location = (loc == 1)?"Box Office":"Online",
                        redemption_date = date,
                        sale_date = (loc == 1)?date:date.AddDays(-r.Next(14)),
                        theme_park = db.theme_park.First(s => s.theme_park_name.Equals("Silicon Shores")),
                        ticket_types = db.ticket_types.ToArray()[r.Next(4)]
                    };
                    db.ticket_sales.Add(sale);
                    db.SaveChanges();
                }
                date = date.AddDays(1);
            }
            ViewBag.duration = DateTime.Now.Subtract(startTime).TotalMilliseconds;
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