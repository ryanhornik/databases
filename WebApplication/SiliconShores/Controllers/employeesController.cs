using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    public class employeesController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: employees
        public ActionResult Index()
        {
            var employees = db.employees.Include(e => e.job_titles).Include(e => e.theme_park);
            return View(employees.ToList());
        }

        // GET: employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: employees/Create
        public ActionResult Create()
        {
            ViewBag.job_title_id = new SelectList(db.job_titles, "job_title_id", "job_title");
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name");
            return View();
        }

        // POST: employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ssn,theme_park_id,first_name,last_name,middle_initial,full_time,payrate,hired_date,job_title_id,date_left,rehireable,username,password")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.job_title_id = new SelectList(db.job_titles, "job_title_id", "job_title", employee.job_title_id);
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", employee.theme_park_id);
            return View(employee);
        }

        // GET: employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.job_title_id = new SelectList(db.job_titles, "job_title_id", "job_title", employee.job_title_id);
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", employee.theme_park_id);
            return View(employee);
        }

        // POST: employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ssn,theme_park_id,first_name,last_name,middle_initial,full_time,payrate,hired_date,job_title_id,date_left,rehireable,username,password")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.job_title_id = new SelectList(db.job_titles, "job_title_id", "job_title", employee.job_title_id);
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", employee.theme_park_id);
            return View(employee);
        }

        // GET: employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employee employee = db.employees.Find(id);
            db.employees.Remove(employee);
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
