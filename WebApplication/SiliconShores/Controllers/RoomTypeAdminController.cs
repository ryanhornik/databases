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
    public class RoomTypeAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: RoomTypeAdmin
        public ActionResult Index()
        {
            return View(db.room_types.ToList());
        }

        // GET: RoomTypeAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            room_types room_types = db.room_types.Find(id);
            if (room_types == null)
            {
                return HttpNotFound();
            }
            return View(room_types);
        }

        // GET: RoomTypeAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomTypeAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "room_type_id,room_types_string")] room_types room_types)
        {
            if (ModelState.IsValid)
            {
                db.room_types.Add(room_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room_types);
        }

        // GET: RoomTypeAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            room_types room_types = db.room_types.Find(id);
            if (room_types == null)
            {
                return HttpNotFound();
            }
            return View(room_types);
        }

        // POST: RoomTypeAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "room_type_id,room_types_string")] room_types room_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room_types);
        }

        // GET: RoomTypeAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            room_types room_types = db.room_types.Find(id);
            if (room_types == null)
            {
                return HttpNotFound();
            }
            return View(room_types);
        }

        // POST: RoomTypeAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            room_types room_types = db.room_types.Find(id);
            db.room_types.Remove(room_types);
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
