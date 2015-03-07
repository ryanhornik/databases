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
    public class WeatherAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: WeatherAdmin
        public ActionResult Index()
        {
            var daily_weather = db.daily_weather.Include(d => d.theme_park);
            return View(daily_weather.ToList());
        }

        // GET: WeatherAdmin/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            daily_weather daily_weather = db.daily_weather.Find(id);
            if (daily_weather == null)
            {
                return HttpNotFound();
            }
            return View(daily_weather);
        }

        // GET: WeatherAdmin/Create
        public ActionResult Create()
        {
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name");
            return View();
        }

        // POST: WeatherAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "weather_date,theme_park_id,weather_conditions,high_temp,low_temp")] daily_weather daily_weather)
        {
            if (ModelState.IsValid)
            {
                db.daily_weather.Add(daily_weather);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", daily_weather.theme_park_id);
            return View(daily_weather);
        }

        // GET: WeatherAdmin/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            daily_weather daily_weather = db.daily_weather.Find(id);
            if (daily_weather == null)
            {
                return HttpNotFound();
            }
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", daily_weather.theme_park_id);
            return View(daily_weather);
        }

        // POST: WeatherAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "weather_date,theme_park_id,weather_conditions,high_temp,low_temp")] daily_weather daily_weather)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daily_weather).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", daily_weather.theme_park_id);
            return View(daily_weather);
        }

        // GET: WeatherAdmin/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            daily_weather daily_weather = db.daily_weather.Find(id);
            if (daily_weather == null)
            {
                return HttpNotFound();
            }
            return View(daily_weather);
        }

        // POST: WeatherAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            daily_weather daily_weather = db.daily_weather.Find(id);
            db.daily_weather.Remove(daily_weather);
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
