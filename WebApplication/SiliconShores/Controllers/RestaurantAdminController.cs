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
    public class RestaurantAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: RestaurantAdmin
        public ActionResult Index()
        {
            var restaurants = db.restaurants.Include(r => r.theme_areas);
            return View(restaurants.ToList());
        }

        // GET: RestaurantAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant restaurant = db.restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: RestaurantAdmin/Create
        public ActionResult Create()
        {
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name");
            return View();
        }

        // POST: RestaurantAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "restaurant_id,restaurant_name,food_category_id,theme_area_id")] restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", restaurant.theme_area_id);
            return View(restaurant);
        }

        // GET: RestaurantAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant restaurant = db.restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", restaurant.theme_area_id);
            return View(restaurant);
        }

        // POST: RestaurantAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "restaurant_id,restaurant_name,food_category_id,theme_area_id")] restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.theme_area_id = new SelectList(db.theme_areas, "theme_area_id", "theme_area_name", restaurant.theme_area_id);
            return View(restaurant);
        }

        // GET: RestaurantAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant restaurant = db.restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: RestaurantAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            restaurant restaurant = db.restaurants.Find(id);
            db.restaurants.Remove(restaurant);
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
