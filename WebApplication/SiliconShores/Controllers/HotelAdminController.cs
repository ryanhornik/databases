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
    public class HotelAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: HotelAdmin
        public ActionResult Index()
        {
            var hotels = db.hotels.Include(h => h.theme_areas);
            return View(hotels.ToList());
        }

        // GET: HotelAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel hotel = db.hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: HotelAdmin/Create
        public ActionResult Create()
        {
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name");
            return View();
        }

        // POST: HotelAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hotel_id,hotel_name,pets_allowed,theme_area_id")] hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", hotel.theme_area_id);
            return View(hotel);
        }

        // GET: HotelAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel hotel = db.hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", hotel.theme_area_id);
            return View(hotel);
        }

        // POST: HotelAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hotel_id,hotel_name,pets_allowed,theme_area_id")] hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", hotel.theme_area_id);
            return View(hotel);
        }

        // GET: HotelAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel hotel = db.hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: HotelAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hotel hotel = db.hotels.Find(id);
            db.hotels.Remove(hotel);
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
