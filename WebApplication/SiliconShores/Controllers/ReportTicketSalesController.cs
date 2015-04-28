using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    [Authorize]
    public class ReportTicketSalesController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();


        // GET: report_ticketsalesAdmin
        public ActionResult Index(int? report)
        {
            return View();
        }


        public ActionResult DisplayTicketSalesWeather(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Params = Request.QueryString.ToString();
            return View();
        }

        public ActionResult DisplayRestaurantSales(DateTime startDate, DateTime endDate)
        {
            ViewBag.Params = Request.QueryString.ToString();
            return View();
        }



        public ActionResult GenerateChart(DateTime startDate, DateTime endDate)
        {
            var ticketReport = db.report_ticketsales
                .Where(r => DateTime.Compare(r.weather_date, startDate) >= 0 &&
                            DateTime.Compare(r.weather_date, endDate) <= 0);

            var totalDaysOfWeatherCondition = new Dictionary<string, int>();

            foreach (var weather in db.daily_weather
                .Where(r => DateTime.Compare(r.weather_date, startDate) >= 0 &&
                            DateTime.Compare(r.weather_date, endDate) <= 0)
                .Select(s => s.weather_conditions))
            {
                var result = 0;
                totalDaysOfWeatherCondition.TryGetValue(weather, out result);
                if (result != 0)
                    totalDaysOfWeatherCondition.Remove(weather);
                totalDaysOfWeatherCondition.Add(weather, result + 1);
            }

            var chartData = new Dictionary<string, int>();
            foreach (var row in ticketReport)
            {
                var result = 0;
                chartData.TryGetValue(row.weather_conditions, out result);
                if (result != 0)
                    chartData.Remove(row.weather_conditions);
                chartData.Add(row.weather_conditions, result + 1);
            }

            String[] xValues = chartData.Keys.ToArray();
            decimal[] yValues = new decimal[xValues.Length];

            for (var k = 0; k < xValues.Length; k++)
            {
                var ticketSales = 0;
                chartData.TryGetValue(xValues[k], out ticketSales);
                var daysOfWeather = 1;
                totalDaysOfWeatherCondition.TryGetValue(xValues[k], out daysOfWeather);
                yValues[k] = (decimal)ticketSales / daysOfWeather;
            }

            var chartTheme = System.IO.File.ReadAllText(Server.MapPath("/Content/chartThemes/defaultTheme.xml"));

            var myChart = new Chart(
                width: 600,
                height: 400,
                theme: chartTheme
                )
                .AddSeries(
                    name: "Ticket Sales",
                    xValue: xValues,
                    yValues: yValues);
            ViewBag.chart = myChart;
            return View();

        }

        public ActionResult GenerateTemperatureReport(DateTime startDate, DateTime endDate)
        {
            var ticketReport = db.ticket_sales
                .Where(r => r.redemption_date != null &&
                            DateTime.Compare(r.redemption_date.Value, startDate) >= 0 &&
                            DateTime.Compare(r.redemption_date.Value, endDate) <= 0).ToList();
            var temperatures = db.daily_weather
                .Where(r => DateTime.Compare(r.weather_date, startDate) >= 0 &&
                            DateTime.Compare(r.weather_date, endDate) <= 0);

            var date = startDate;
            var dataSet = new List<KeyValuePair<KeyValuePair<int, int>, int>>();
            foreach (var temp in temperatures)
            {
                var sales = ticketReport.Count(r => DateTime.Compare(r.redemption_date.Value, temp.weather_date) == 0);
                dataSet.Add(new KeyValuePair<KeyValuePair<int, int>, int>(
                    new KeyValuePair<int, int>(temp.low_temp, temp.high_temp), sales));
                date = date.AddDays(1);
            }





            var lowTemps = new List<int>();
            var yAxisSales = new List<int>();
            var highTemps = new List<int>();

            foreach (var point in dataSet)
            {
                lowTemps.Add(point.Key.Key); // yolo
                highTemps.Add(point.Key.Value);
                yAxisSales.Add(point.Value);
            }

            var chartTheme = System.IO.File.ReadAllText(Server.MapPath("/Content/chartThemes/defaultTheme.xml"));

            var myChart = new Chart(
                width: 600,
                height: 400,
                theme: chartTheme
                )
                .AddSeries(
                    name: "Low Temperatures",
                    chartType: "Point",
                    xValue: lowTemps.ToArray(),
                    yValues: yAxisSales.ToArray())
                .AddSeries(
                    name: "High Temperatures",
                    chartType: "Point",
                    xValue: highTemps.ToArray(),
                    yValues: yAxisSales.ToArray()
                ).AddLegend("High/Low", "Temp");
            ViewBag.chart = myChart;
            return View();
        }



        public ActionResult GenerateRestReport1(DateTime startDate, DateTime endDate)
        {
        
            var restReport = db.restaurant_daily_reports
                .Where(r => r.report_date != null &&
                            DateTime.Compare(r.report_date, startDate) >= 0 &&
                            DateTime.Compare(r.report_date, endDate) <= 0).ToList();

     



            var dataSet = new Dictionary<int, KeyValuePair<int,int>>();
     
            foreach(var report in restReport)
            {
                var result = new KeyValuePair<int, int>();


                dataSet.TryGetValue(report.restaurant_id, out result);
                if(result.Key!=0 && result.Value!=0)
                {
                    dataSet.Remove(report.restaurant_id);
                }
                dataSet.Add(report.restaurant_id, new KeyValuePair<int, int>(result.Key + (int)report.gross_income,
                    result.Value + report.patrons_served));
            }
            
                
            
           
        

            var xAxisOne = new List<int>();
            var yAxisOne = new List<int>();
            var yAxisTwo = new List<int>();


            foreach (var point in dataSet)
            {
                xAxisOne.Add(point.Key);
                yAxisOne.Add(point.Value.Key); 
                yAxisTwo.Add(point.Value.Value);
            }



            var chartTheme = System.IO.File.ReadAllText(Server.MapPath("/Content/chartThemes/defaultTheme.xml"));

   
            var myChart = new Chart(
                width: 600,
                height: 400,
                theme: chartTheme
                )
                .AddSeries(
                    name: "Patrons Served",
                    xValue:  xAxisOne.ToArray(),
                    yValues: yAxisTwo.ToArray()
                ).AddLegend("1 - MegaByte \n 2- The Stack \n 3- Gone Phishing \n 4 - Taco Bell \n 5 - The Agora \n 6 - Szechuan");



            ViewBag.chart = myChart;

            return View();
        }


        public ActionResult GenerateRestReport2(DateTime startDate, DateTime endDate)
        {

            var restReport = db.restaurant_daily_reports
                .Where(r => r.report_date != null &&
                            DateTime.Compare(r.report_date, startDate) >= 0 &&
                            DateTime.Compare(r.report_date, endDate) <= 0).ToList();





            var dataSet = new Dictionary<int, KeyValuePair<int, int>>();

            foreach (var report in restReport)
            {
                var result = new KeyValuePair<int, int>();


                dataSet.TryGetValue(report.restaurant_id, out result);
                if (result.Key != 0 && result.Value != 0)
                {
                    dataSet.Remove(report.restaurant_id);
                }
                dataSet.Add(report.restaurant_id, new KeyValuePair<int, int>(result.Key + (int)report.gross_income,
                    result.Value + report.patrons_served));
            }






            var xAxisOne = new List<int>();
            var yAxisOne = new List<int>();
            var yAxisTwo = new List<int>();


            foreach (var point in dataSet)
            {
                xAxisOne.Add(point.Key);
                yAxisOne.Add(point.Value.Key);
                yAxisTwo.Add(point.Value.Value);
            }

       

            var chartTheme = System.IO.File.ReadAllText(Server.MapPath("/Content/chartThemes/defaultTheme.xml"));

            var myChart = new Chart(
                width: 600,
                height: 400,
                theme: chartTheme
                )
                .AddSeries(
                    name: "Gross Income",
                    xValue: xAxisOne.ToArray(),
                    yValues: yAxisOne.ToArray()).AddLegend("1 - MegaByte \n 2- The Stack \n 3- Gone Phishing \n 4 - Taco Bell \n 5 - The Agora \n 6 - Szechuan");




            ViewBag.chart = myChart;
       




            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

