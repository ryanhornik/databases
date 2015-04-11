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
            var ticketReport = db.report_ticketsales.Include(r => r.ticket_id);
            return View(ticketReport.ToList());
        }

        // GET: report_ticketsalesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report_ticketsale ticketReport = db.report_ticketsales.Find(id);
            if (ticketReport == null)
            {
                return HttpNotFound();
            }
            return View(ticketReport);
        }

        // GET: report_ticketsalesAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ticket_id = new SelectList(db.ticket_sales, "ticket_id", "ticket_type_id");
            return View();
        }

        // POST: report_ticketsalesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "report_ticketsales_id,ticket_id,incidence_date,resolution_date,repair_cost")] report_ticketsale report_ticketsales)
        {
            if (ModelState.IsValid)
            {
                db.report_ticketsales.Add(report_ticketsales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ticket_id = new SelectList(db.attractions, "attractions_id", "attraction_name", report_ticketsales.ticket_id);
            return View(report_ticketsales);
        }

        // GET: report_ticketsalesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report_ticketsale report_ticketsales = db.report_ticketsales.Find(id);
            if (report_ticketsales == null)
            {
                return HttpNotFound();
            }
            ViewBag.ticket_id = new SelectList(db.attractions, "attractions_id", "attraction_name", report_ticketsales.ticket_id);
            return View(report_ticketsales);
        }

        // POST: report_ticketsalesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "report_ticketsales_id,ticket_id,incidence_date,resolution_date,repair_cost")] report_ticketsale report_ticketsales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report_ticketsales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ticket_id = new SelectList(db.attractions, "attractions_id", "attraction_name", report_ticketsales.ticket_id);
            return View(report_ticketsales);
        }

        // GET: report_ticketsalesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report_ticketsale report_ticketsales = db.report_ticketsales.Find(id);
            if (report_ticketsales == null)
            {
                return HttpNotFound();
            }
            return View(report_ticketsales);
        }

        // POST: report_ticketsalesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            report_ticketsale report_ticketsales = db.report_ticketsales.Find(id);
            db.report_ticketsales.Remove(report_ticketsales);
            db.SaveChanges();
            return RedirectToAction("Index");
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

