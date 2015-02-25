using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using SiliconShores.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<employee>("employees");
    builder.EntitySet<job_titles>("job_titles"); 
    builder.EntitySet<theme_park>("theme_park"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class employeesController : ODataController
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: odata/employees
        [EnableQuery]
        public IQueryable<employee> Getemployees()
        {
            return db.employees;
        }

        // GET: odata/employees(5)
        [EnableQuery]
        public SingleResult<employee> Getemployee([FromODataUri] int key)
        {
            return SingleResult.Create(db.employees.Where(employee => employee.ssn == key));
        }

        // PUT: odata/employees(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<employee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employee employee = db.employees.Find(key);
            if (employee == null)
            {
                return NotFound();
            }

            patch.Put(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employee);
        }

        // POST: odata/employees
        public IHttpActionResult Post(employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.employees.Add(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (employeeExists(employee.ssn))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(employee);
        }

        // PATCH: odata/employees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<employee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employee employee = db.employees.Find(key);
            if (employee == null)
            {
                return NotFound();
            }

            patch.Patch(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employee);
        }

        // DELETE: odata/employees(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            employee employee = db.employees.Find(key);
            if (employee == null)
            {
                return NotFound();
            }

            db.employees.Remove(employee);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/employees(5)/job_titles
        [EnableQuery]
        public SingleResult<job_titles> Getjob_titles([FromODataUri] int key)
        {
            return SingleResult.Create(db.employees.Where(m => m.ssn == key).Select(m => m.job_titles));
        }

        // GET: odata/employees(5)/theme_park
        [EnableQuery]
        public SingleResult<theme_park> Gettheme_park([FromODataUri] int key)
        {
            return SingleResult.Create(db.employees.Where(m => m.ssn == key).Select(m => m.theme_park));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool employeeExists(int key)
        {
            return db.employees.Count(e => e.ssn == key) > 0;
        }
    }
}
