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
            var themePark =  db.theme_park.First(s => s.theme_park_name.Equals("Silicon Shores"));
            if (themePark == null)
            {
                return HttpNotFound();
            }
            return View(themePark);
        }
    }
}