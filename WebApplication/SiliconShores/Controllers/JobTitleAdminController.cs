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
    public class JobTitleAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: JobTitleAdmin
        public ActionResult Index()
        {
            return View(db.job_titles.ToList());
        }

        // GET: JobTitleAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job_titles job_titles = db.job_titles.Find(id);
            if (job_titles == null)
            {
                return HttpNotFound();
            }
            return View(job_titles);
        }

        // GET: JobTitleAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobTitleAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_title_id,job_title")] job_titles job_titles)
        {
            if (ModelState.IsValid)
            {
                db.job_titles.Add(job_titles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job_titles);
        }

        // GET: JobTitleAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job_titles job_titles = db.job_titles.Find(id);
            if (job_titles == null)
            {
                return HttpNotFound();
            }
            return View(job_titles);
        }

        // POST: JobTitleAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_title_id,job_title")] job_titles job_titles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job_titles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job_titles);
        }

        // GET: JobTitleAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job_titles job_titles = db.job_titles.Find(id);
            if (job_titles == null)
            {
                return HttpNotFound();
            }
            return View(job_titles);
        }

        // POST: JobTitleAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            job_titles job_titles = db.job_titles.Find(id);
            db.job_titles.Remove(job_titles);
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
