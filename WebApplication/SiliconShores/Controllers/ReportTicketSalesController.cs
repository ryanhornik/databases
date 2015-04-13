using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SiliconShores.Models;

//  [Authorize]
public class ReportTicketSalesController : Controller
{
    private theme_park_dbEntities db = new theme_park_dbEntities();

    // GET: report_ticketsalesAdmin
    public ActionResult Index()
    {
        var ticketReport = db.report_ticketsales.Select(r => r);
        return View(ticketReport.ToList());
    }

    // GET: report_ticketsalesAdmin/Details/5
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        report_ticketsales ticketReport = db.report_ticketsales.Find(id);
        if (ticketReport == null)
        {
            return HttpNotFound();
        }
        return View(ticketReport);
    }

    public ActionResult DisplayTicketSalesWeather()
    {
        return View();
    }

    public ActionResult GenerateChart()
    {
        var ticketReport = db.report_ticketsales.Select(r => r);
        var totalDaysOfWeatherCondition = new Dictionary<string, int>();

        foreach (var weather in db.daily_weather.Select(s => s.weather_conditions))
        {
            var result = 0;
            totalDaysOfWeatherCondition.TryGetValue(weather, out result);
            if(result != 0)
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
            yValues[k] = (decimal)ticketSales/daysOfWeather;
        }

        var myChart = new Chart(
            width: 1000,
            height: 800,
            theme: ChartTheme.Blue)
        .AddTitle("Ticket Redemption by Weather")
        .AddSeries(
            name: "Ticket Sales",
            xValue: xValues,
            yValues: yValues);
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

