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
    public class HotelReservationAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: HotelReservationAdmin
        public ActionResult Index()
        {
            var hotel_reservations = db.hotel_reservations.Include(h => h.hotel_rooms);
            return View(hotel_reservations.ToList());
        }

        // GET: HotelReservationAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel_reservations hotel_reservations = db.hotel_reservations.Find(id);
            if (hotel_reservations == null)
            {
                return HttpNotFound();
            }
            return View(hotel_reservations);
        }

        // GET: HotelReservationAdmin/Create
        public ActionResult Create()
        {
            ViewBag.hotel_id = new SelectList(db.hotel_rooms, "hotel_id", "hotel_id");
            return View();
        }

        // POST: HotelReservationAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reservation_id,hotel_id,room_number,reservation_checkin_date,reservation_checkout_date,total_reservation_cost,paid_in_full")] hotel_reservations hotel_reservations)
        {
            if (ModelState.IsValid)
            {
                db.hotel_reservations.Add(hotel_reservations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.hotel_id = new SelectList(db.hotel_rooms, "hotel_id", "hotel_id", hotel_reservations.hotel_id);
            return View(hotel_reservations);
        }

        // GET: HotelReservationAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel_reservations hotel_reservations = db.hotel_reservations.Find(id);
            if (hotel_reservations == null)
            {
                return HttpNotFound();
            }
            ViewBag.hotel_id = new SelectList(db.hotel_rooms, "hotel_id", "hotel_id", hotel_reservations.hotel_id);
            return View(hotel_reservations);
        }

        // POST: HotelReservationAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reservation_id,hotel_id,room_number,reservation_checkin_date,reservation_checkout_date,total_reservation_cost,paid_in_full")] hotel_reservations hotel_reservations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel_reservations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hotel_id = new SelectList(db.hotel_rooms, "hotel_id", "hotel_id", hotel_reservations.hotel_id);
            return View(hotel_reservations);
        }

        // GET: HotelReservationAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel_reservations hotel_reservations = db.hotel_reservations.Find(id);
            if (hotel_reservations == null)
            {
                return HttpNotFound();
            }
            return View(hotel_reservations);
        }

        // POST: HotelReservationAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hotel_reservations hotel_reservations = db.hotel_reservations.Find(id);
            db.hotel_reservations.Remove(hotel_reservations);
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
