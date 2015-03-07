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
    public class ThemeParkAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: ThemeParkAdmin
        public ActionResult Index()
        {
            return View(db.theme_park.ToList());
        }

        // GET: ThemeParkAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_park theme_park = db.theme_park.Find(id);
            if (theme_park == null)
            {
                return HttpNotFound();
            }
            return View(theme_park);
        }

        // GET: ThemeParkAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThemeParkAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "theme_park_id,theme_park_name,park_open,park_close,park_country,park_state_or_province,park_city,park_street_address")] theme_park theme_park)
        {
            if (ModelState.IsValid)
            {
                db.theme_park.Add(theme_park);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theme_park);
        }

        // GET: ThemeParkAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_park theme_park = db.theme_park.Find(id);
            if (theme_park == null)
            {
                return HttpNotFound();
            }
            return View(theme_park);
        }

        // POST: ThemeParkAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "theme_park_id,theme_park_name,park_open,park_close,park_country,park_state_or_province,park_city,park_street_address")] theme_park theme_park)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theme_park).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theme_park);
        }

        // GET: ThemeParkAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_park theme_park = db.theme_park.Find(id);
            if (theme_park == null)
            {
                return HttpNotFound();
            }
            return View(theme_park);
        }

        // POST: ThemeParkAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            theme_park theme_park = db.theme_park.Find(id);
            db.theme_park.Remove(theme_park);
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
