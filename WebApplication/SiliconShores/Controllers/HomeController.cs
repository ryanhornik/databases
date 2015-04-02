using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    public class HomeController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();
        public ActionResult Index()
        {
            var theme_parks = from m in db.theme_park
                              select m;
            theme_parks = theme_parks.Where(s => s.theme_park_name.Equals("Silicon Shores"));
            theme_park themePark = theme_parks.First();
            if (themePark == null)
            {
                return HttpNotFound();
            }
            return View(themePark);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

            return View();
        }

        public ActionResult Contact()
        {
            
            ViewBag.Message = "Contact us";

            return View();
        }
    }
}