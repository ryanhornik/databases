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
    [Authorize]
    public class RestaurantReportAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: RestaurantReportAdmin
        public ActionResult Index()
        {
            var restaurant_daily_reports = db.restaurant_daily_reports.Include(r => r.restaurant);
            return View(restaurant_daily_reports.ToList());
        }

        // GET: RestaurantReportAdmin/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant_daily_reports restaurant_daily_reports = db.restaurant_daily_reports.Find(id);
            if (restaurant_daily_reports == null)
            {
                return HttpNotFound();
            }
            return View(restaurant_daily_reports);
        }

        // GET: RestaurantReportAdmin/Create
        public ActionResult Create()
        {
            ViewBag.restaurant_id = new SelectList(db.restaurants, "restaurant_id", "restaurant_name");
            return View();
        }

        // POST: RestaurantReportAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "report_date,restaurant_id,gross_income,patrons_served")] restaurant_daily_reports restaurant_daily_reports)
        {
            if (ModelState.IsValid)
            {
                db.restaurant_daily_reports.Add(restaurant_daily_reports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.restaurant_id = new SelectList(db.restaurants, "restaurant_id", "restaurant_name", restaurant_daily_reports.restaurant_id);
            return View(restaurant_daily_reports);
        }

        // GET: RestaurantReportAdmin/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant_daily_reports restaurant_daily_reports = db.restaurant_daily_reports.Find(id);
            if (restaurant_daily_reports == null)
            {
                return HttpNotFound();
            }
            ViewBag.restaurant_id = new SelectList(db.restaurants, "restaurant_id", "restaurant_name", restaurant_daily_reports.restaurant_id);
            return View(restaurant_daily_reports);
        }

        // POST: RestaurantReportAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "report_date,restaurant_id,gross_income,patrons_served")] restaurant_daily_reports restaurant_daily_reports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant_daily_reports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.restaurant_id = new SelectList(db.restaurants, "restaurant_id", "restaurant_name", restaurant_daily_reports.restaurant_id);
            return View(restaurant_daily_reports);
        }

        // GET: RestaurantReportAdmin/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant_daily_reports restaurant_daily_reports = db.restaurant_daily_reports.Find(id);
            if (restaurant_daily_reports == null)
            {
                return HttpNotFound();
            }
            return View(restaurant_daily_reports);
        }

        // POST: RestaurantReportAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            restaurant_daily_reports restaurant_daily_reports = db.restaurant_daily_reports.Find(id);
            db.restaurant_daily_reports.Remove(restaurant_daily_reports);
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
