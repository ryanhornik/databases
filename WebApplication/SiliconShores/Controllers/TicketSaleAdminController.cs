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
    public class TicketSaleAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: TicketSaleAdmin
        public ActionResult Index()
        {
            var ticket_sales = db.ticket_sales.Include(t => t.theme_park).Include(t => t.ticket_types);
            return View(ticket_sales.ToList());
        }

        // GET: TicketSaleAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_sales ticket_sales = db.ticket_sales.Find(id);
            if (ticket_sales == null)
            {
                return HttpNotFound();
            }
            return View(ticket_sales);
        }

        // GET: TicketSaleAdmin/Create
        public ActionResult Create()
        {
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name");
            ViewBag.ticket_type_id = new SelectList(db.ticket_types, "ticket_type_id", "ticket_name");
            return View();
        }

        // POST: TicketSaleAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ticket_id,ticket_type_id,sale_date,redemption_date,theme_park_id,sale_location")] ticket_sales ticket_sales)
        {
            if (ModelState.IsValid)
            {
                db.ticket_sales.Add(ticket_sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", ticket_sales.theme_park_id);
            ViewBag.ticket_type_id = new SelectList(db.ticket_types, "ticket_type_id", "ticket_name", ticket_sales.ticket_type_id);
            return View(ticket_sales);
        }

        // GET: TicketSaleAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_sales ticket_sales = db.ticket_sales.Find(id);
            if (ticket_sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", ticket_sales.theme_park_id);
            ViewBag.ticket_type_id = new SelectList(db.ticket_types, "ticket_type_id", "ticket_name", ticket_sales.ticket_type_id);
            return View(ticket_sales);
        }

        // POST: TicketSaleAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ticket_id,ticket_type_id,sale_date,redemption_date,theme_park_id,sale_location")] ticket_sales ticket_sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket_sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.theme_park_id = new SelectList(db.theme_park, "theme_park_id", "theme_park_name", ticket_sales.theme_park_id);
            ViewBag.ticket_type_id = new SelectList(db.ticket_types, "ticket_type_id", "ticket_name", ticket_sales.ticket_type_id);
            return View(ticket_sales);
        }

        // GET: TicketSaleAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_sales ticket_sales = db.ticket_sales.Find(id);
            if (ticket_sales == null)
            {
                return HttpNotFound();
            }
            return View(ticket_sales);
        }

        // POST: TicketSaleAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ticket_sales ticket_sales = db.ticket_sales.Find(id);
            db.ticket_sales.Remove(ticket_sales);
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
