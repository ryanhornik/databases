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
    public class SectionsController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: Sections
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            theme_areas theme_areas = db.theme_areas.Find(id);
            if (theme_areas == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.ImageList = theme_areas.getImagePaths();
            

            return View(theme_areas);
        }

        //Retrieves a list of the theme_areas, Should use to modify _layout.cshtml
        //var theme_areas = db.theme_areas.Include(t => t.theme_park);
        //return View(theme_areas.ToList());
    }
}