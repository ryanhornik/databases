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
    public class AttractionAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: AttractionsAdmin
        public ActionResult Index()
        {
            var attractions = db.attractions.Include(a => a.theme_areas);
            return View(attractions.ToList());
        }

        // GET: AttractionsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attraction attraction = db.attractions.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            return View(attraction);
        }

        // GET: AttractionsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name");
            return View();
        }

        // POST: AttractionsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "attractions_id,attraction_name,theme_area_id,attraction_description,picture_path,is_working,date_opened")] attraction attraction)
        {
            if (ModelState.IsValid)
            {
                db.attractions.Add(attraction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", attraction.theme_area_id);
            return View(attraction);
        }

        // GET: AttractionsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attraction attraction = db.attractions.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", attraction.theme_area_id);
            return View(attraction);
        }

        // POST: AttractionsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "attractions_id,attraction_name,theme_area_id,attraction_description,picture_path,is_working,date_opened")] attraction attraction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attraction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", attraction.theme_area_id);
            return View(attraction);
        }

        // GET: AttractionsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attraction attraction = db.attractions.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            return View(attraction);
        }

        // POST: AttractionsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            attraction attraction = db.attractions.Find(id);
            db.attractions.Remove(attraction);
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
