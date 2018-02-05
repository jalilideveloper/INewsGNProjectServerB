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
    public class tblNewsLocationsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblNewsLocations
        public IQueryable<tblNewsLocation> GettblNewsLocation()
        {
            return db.tblNewsLocations;
        }

        // GET: api/tblNewsLocations/5
        [ResponseType(typeof(tblNewsLocation))]
        public IHttpActionResult GettblNewsLocation(int id)
        {
            var tblNewsLocation = db.GetByIdNewsLocation(id);
            if (tblNewsLocation == null)
            {
                return NotFound();
            }

            return Ok(tblNewsLocation);
        }

        // PUT: api/tblNewsLocations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblNewsLocation(int id, tblNewsLocation tblNewsLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblNewsLocation.NewsLocationID)
            {
                return BadRequest();
            }

            db.Entry(tblNewsLocation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblNewsLocationExists(id))
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

        // POST: api/tblNewsLocations
        [ResponseType(typeof(tblNewsLocation))]
        public IHttpActionResult PosttblNewsLocation(tblNewsLocation tblNewsLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblNewsLocation(tblNewsLocation.NewsID, tblNewsLocation.LocationID, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblNewsLocation.NewsLocationID }, tblNewsLocation);
        }

        // DELETE: api/tblNewsLocations/5
        [ResponseType(typeof(tblNewsLocation))]
        public IHttpActionResult DeletetblNewsLocation(int id)
        {
            db.sp_DeltblNewsLocation(id);
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

        private bool tblNewsLocationExists(int id)
        {
            return db.tblNewsLocations.Count(e => e.NewsLocationID == id) > 0;
        }
    }
}