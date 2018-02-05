using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using INewGN.Models;

namespace INewGN.Controllers
{
    public class tblPagePostLocationsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblPagePostLocations
        public IQueryable<tblPagePostLocation> GettblPagePostLocations()
        {
            return db.tblPagePostLocations;
        }

        // GET: api/tblPagePostLocations/5
        [ResponseType(typeof(tblPagePostLocation))]
        public IHttpActionResult GettblPagePostLocations(int id)
        {
            var tblPagePostLocations = db.GetByIdPagePostLocations(id);
            if (tblPagePostLocations == null)
            {
                return NotFound();
            }

            return Ok(tblPagePostLocations);
        }

        // PUT: api/tblPagePostLocations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblPagePostLocations(int id, tblPagePostLocation tblPagePostLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblPagePostLocations.PagePostLocID)
            {
                return BadRequest();
            }

            db.Entry(tblPagePostLocations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblPagePostLocationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblPagePostLocations
        [ResponseType(typeof(tblPagePostLocation))]
        public IHttpActionResult PosttblPagePostLocations(tblPagePostLocation tblPagePostLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblPagePostLocations(tblPagePostLocations.LocationID, tblPagePostLocations.PagePostID, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblPagePostLocations.PagePostLocID }, tblPagePostLocations);
        }

        // DELETE: api/tblPagePostLocations/5
        [ResponseType(typeof(tblPagePostLocation))]
        public IHttpActionResult DeletetblPagePostLocations(int id)
        {
            db.sp_DeltblPagePostLocations(id);
            return Ok(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblPagePostLocationsExists(int id)
        {
            return db.tblPagePostLocations.Count(e => e.PagePostLocID == id) > 0;
        }
    }
}