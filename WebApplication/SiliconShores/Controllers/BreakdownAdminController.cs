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
    public class BreakdownAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: BreakdownAdmin
        public ActionResult Index()
        {
            var breakdowns = db.breakdowns.Include(b => b.attraction);
            return View(breakdowns.ToList());
        }

        // GET: BreakdownAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            breakdown breakdown = db.breakdowns.Find(id);
            if (breakdown == null)
            {
                return HttpNotFound();
            }
            return View(breakdown);
        }

        // GET: BreakdownAdmin/Create
        public ActionResult Create()
        {
            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name");
            return View();
        }

        // POST: BreakdownAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "breakdown_id,attraction_id,incidence_date,resolution_date,repair_cost")] breakdown breakdown)
        {
            if (ModelState.IsValid)
            {
                db.breakdowns.Add(breakdown);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name", breakdown.attraction_id);
            return View(breakdown);
        }

        // GET: BreakdownAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            breakdown breakdown = db.breakdowns.Find(id);
            if (breakdown == null)
            {
                return HttpNotFound();
            }
            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name", breakdown.attraction_id);
            return View(breakdown);
        }

        // POST: BreakdownAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "breakdown_id,attraction_id,incidence_date,resolution_date,repair_cost")] breakdown breakdown)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breakdown).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.attraction_id = new SelectList(db.attractions, "attractions_id", "attraction_name", breakdown.attraction_id);
            return View(breakdown);
        }

        // GET: BreakdownAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            breakdown breakdown = db.breakdowns.Find(id);
            if (breakdown == null)
            {
                return HttpNotFound();
            }
            return View(breakdown);
        }

        // POST: BreakdownAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            breakdown breakdown = db.breakdowns.Find(id);
            db.breakdowns.Remove(breakdown);
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
