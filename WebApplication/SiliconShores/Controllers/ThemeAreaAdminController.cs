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
    public class ThemeAreaAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: ThemeAreaAdmin
        public ActionResult Index()
        {
            var theme_areas = db.theme_areas.Include(t => t.theme_park);
            return View(theme_areas.ToList());
        }

        // GET: ThemeAreaAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_areas theme_areas = db.theme_areas.Find(id);
            if (theme_areas == null)
            {
                return HttpNotFound();
            }
            return View(theme_areas);
        }

        // GET: ThemeAreaAdmin/Create
        public ActionResult Create()
        {
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name");
            return View();
        }

        // POST: ThemeAreaAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "theme_area_id,theme_area_name,theme_area_description,theme_area_pictures,theme_park_id")] theme_areas theme_areas)
        {
            if (ModelState.IsValid)
            {
                db.theme_areas.Add(theme_areas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", theme_areas.theme_park_id);
            return View(theme_areas);
        }

        // GET: ThemeAreaAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_areas theme_areas = db.theme_areas.Find(id);
            if (theme_areas == null)
            {
                return HttpNotFound();
            }
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", theme_areas.theme_park_id);
            return View(theme_areas);
        }

        // POST: ThemeAreaAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "theme_area_id,theme_area_name,theme_area_description,theme_area_pictures,theme_park_id")] theme_areas theme_areas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theme_areas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", theme_areas.theme_park_id);
            return View(theme_areas);
        }

        // GET: ThemeAreaAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_areas theme_areas = db.theme_areas.Find(id);
            if (theme_areas == null)
            {
                return HttpNotFound();
            }
            return View(theme_areas);
        }

        // POST: ThemeAreaAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            theme_areas theme_areas = db.theme_areas.Find(id);
            db.theme_areas.Remove(theme_areas);
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
