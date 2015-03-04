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
    public class TicketTypeAdminController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: TicketTypeAdmin
        public ActionResult Index()
        {
            return View(db.ticket_types.ToList());
        }

        // GET: TicketTypeAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_types ticket_types = db.ticket_types.Find(id);
            if (ticket_types == null)
            {
                return HttpNotFound();
            }
            return View(ticket_types);
        }

        // GET: TicketTypeAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketTypeAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ticket_type_id,ticket_name,ticket_restrictions,ticket_price")] ticket_types ticket_types)
        {
            if (ModelState.IsValid)
            {
                db.ticket_types.Add(ticket_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket_types);
        }

        // GET: TicketTypeAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_types ticket_types = db.ticket_types.Find(id);
            if (ticket_types == null)
            {
                return HttpNotFound();
            }
            return View(ticket_types);
        }

        // POST: TicketTypeAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ticket_type_id,ticket_name,ticket_restrictions,ticket_price")] ticket_types ticket_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket_types);
        }

        // GET: TicketTypeAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_types ticket_types = db.ticket_types.Find(id);
            if (ticket_types == null)
            {
                return HttpNotFound();
            }
            return View(ticket_types);
        }

        // POST: TicketTypeAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ticket_types ticket_types = db.ticket_types.Find(id);
            db.ticket_types.Remove(ticket_types);
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
