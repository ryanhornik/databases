using SiliconShores.Models;
using System.Data.Entity;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiliconShores.Controllers
{
    public class PlanVacationController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: PlanVacation
        public ActionResult Index(string submit)
        {
            //ViewBag.ticket_types_id = new SelectList(db.ticket_types, "ticket_types_id", "ticket_name");
            ViewBag.RoomTypes = new SelectList(db.room_types, "room_type_id", "room_types_string");
            return View();
        }
    }
}