using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    public class RideReportAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: RideReportAdmin
        public ActionResult Index()
        {
            var daily_ride_report = db.daily_ride_report.Include(d => d.attraction);
            return View(daily_ride_report.ToList());
        }

        // GET: RideReportAdmin/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            daily_ride_report daily_ride_report = db.daily_ride_report.Find(id);
            if (daily_ride_report == null)
            {
                return HttpNotFound();
            }
            return View(daily_ride_report);
        }

        // GET: RideReportAdmin/Create
        public ActionResult Create()
        {
            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name");
            return View();
        }

        // POST: RideReportAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ride_report_date,attraction_id,total_riders")] daily_ride_report daily_ride_report)
        {
            if (ModelState.IsValid)
            {
                db.daily_ride_report.Add(daily_ride_report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name", daily_ride_report.attraction_id);
            return View(daily_ride_report);
        }

        // GET: RideReportAdmin/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            daily_ride_report daily_ride_report = db.daily_ride_report.Find(id);
            if (daily_ride_report == null)
            {
                return HttpNotFound();
            }
            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name", daily_ride_report.attraction_id);
            return View(daily_ride_report);
        }

        // POST: RideReportAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ride_report_date,attraction_id,total_riders")] daily_ride_report daily_ride_report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daily_ride_report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name", daily_ride_report.attraction_id);
            return View(daily_ride_report);
        }

        // GET: RideReportAdmin/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            daily_ride_report daily_ride_report = db.daily_ride_report.Find(id);
            if (daily_ride_report == null)
            {
                return HttpNotFound();
            }
            return View(daily_ride_report);
        }

        // POST: RideReportAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            daily_ride_report daily_ride_report = db.daily_ride_report.Find(id);
            db.daily_ride_report.Remove(daily_ride_report);
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
}
