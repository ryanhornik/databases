using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiliconShores.Models;

  //  [Authorize]
    public class ReportTicketSalesController: Controller
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
            
            var ticketReport = db.report_ticketsales.Select(r => r);
            SortedList<string, int> chartData = new SortedList<string, int>();

            foreach(var row in ticketReport)
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

            report_ticketsalesChartData chartDataModel = new report_ticketsalesChartData();
            chartDataModel.xValues = chartData.Keys.ToArray<String>();
            chartDataModel.yValues = chartData.Values.ToArray<int>();
       

            return View(chartDataModel);
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

