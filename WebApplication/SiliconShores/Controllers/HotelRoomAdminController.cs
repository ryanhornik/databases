using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    //[Authorize]
    public class HotelRoomAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: HotelRoomAdmin
        public ActionResult Index(string genericSearch)
        {
            var hotelRooms= db.hotel_rooms.ToList();
            var roomsToPrint = hotelRooms.Skip(new Random().Next(hotelRooms.Count - 15)).Take(15).ToList();
            if (!genericSearch.IsEmpty())
            {
                roomsToPrint = hotelRooms.Where(s => s.fullSearchString().Contains(genericSearch)).ToList();
            }
            return View(roomsToPrint);
        }

        // GET: HotelRoomAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel_rooms hotel_rooms = db.hotel_rooms.Find(id);
            if (hotel_rooms == null)
            {
                return HttpNotFound();
            }
            return View(hotel_rooms);
        }

        // GET: HotelRoomAdmin/Create
        public ActionResult Create()
        {
            ViewBag.hotel_id = new SelectList(db.hotels, "hotel_id", "hotel_name");
            ViewBag.room_type_id = new SelectList(db.room_types, "room_type_id", "room_types_string");
            return View();
        }

        // POST: HotelRoomAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hotel_id,room_number,room_type_id,room_rate,occupied")] hotel_rooms hotel_rooms)
        {
            if (ModelState.IsValid)
            {
                db.hotel_rooms.Add(hotel_rooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.hotel_id = new SelectList(db.hotels, "hotel_id", "hotel_name", hotel_rooms.hotel_id);
            ViewBag.room_type_id = new SelectList(db.room_types, "room_type_id", "room_types_string", hotel_rooms.room_type_id);
            return View(hotel_rooms);
        }

        // GET: HotelRoomAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel_rooms hotel_rooms = db.hotel_rooms.Find(id);
            if (hotel_rooms == null)
            {
                return HttpNotFound();
            }
            ViewBag.hotel_id = new SelectList(db.hotels, "hotel_id", "hotel_name", hotel_rooms.hotel_id);
            ViewBag.room_type_id = new SelectList(db.room_types, "room_type_id", "room_types_string", hotel_rooms.room_type_id);
            return View(hotel_rooms);
        }

        // POST: HotelRoomAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hotel_id,room_number,room_type_id,room_rate,occupied")] hotel_rooms hotel_rooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel_rooms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hotel_id = new SelectList(db.hotels, "hotel_id", "hotel_name", hotel_rooms.hotel_id);
            ViewBag.room_type_id = new SelectList(db.room_types, "room_type_id", "room_types_string", hotel_rooms.room_type_id);
            return View(hotel_rooms);
        }

        // GET: HotelRoomAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel_rooms hotel_rooms = db.hotel_rooms.Find(id);
            if (hotel_rooms == null)
            {
                return HttpNotFound();
            }
            return View(hotel_rooms);
        }

        // POST: HotelRoomAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hotel_rooms hotel_rooms = db.hotel_rooms.Find(id);
            db.hotel_rooms.Remove(hotel_rooms);
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
