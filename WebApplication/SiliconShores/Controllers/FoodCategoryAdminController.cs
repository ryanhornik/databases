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
    public class FoodCategoryAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: FoodCategoryAdmin
        public ActionResult Index()
        {
            return View(db.food_categories.ToList());
        }

        // GET: FoodCategoryAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }

        // GET: FoodCategoryAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodCategoryAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "food_category_id,food_categories_name")] food_categories food_categories)
        {
            if (ModelState.IsValid)
            {
                db.food_categories.Add(food_categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food_categories);
        }

        // GET: FoodCategoryAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }

        // POST: FoodCategoryAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "food_category_id,food_categories_name")] food_categories food_categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food_categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food_categories);
        }

        // GET: FoodCategoryAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }

        // POST: FoodCategoryAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            food_categories food_categories = db.food_categories.Find(id);
            db.food_categories.Remove(food_categories);
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
