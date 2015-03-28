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
    public class HomeController : Controller

    {
        private theme_park_dbEntities db = new theme_park_dbEntities();
        public ActionResult Index(char? name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_park theme_park = db.theme_park.Find("Silicon Shores");
            if (theme_park == null)
            {
                return HttpNotFound();
            }
            return View(theme_park);
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