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
        SortedList<string, int> chartData = new SortedList<string, int>();

        foreach (var row in ticketReport)
        {
            if (!chartData.ContainsKey(row.weather_conditions))
            {
                chartData.Add(row.weather_conditions, 1);
            }
            else
            {
                chartData[row.weather_conditions]++;
            }
        }

        String[] xValues = chartData.Keys.ToArray<String>();
        int[] yValues = chartData.Values.ToArray<int>();

        var myChart = new Chart(width: 400, height: 400)
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

