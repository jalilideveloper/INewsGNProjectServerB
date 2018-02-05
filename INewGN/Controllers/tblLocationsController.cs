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
    public class tblLocationsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblLocations
        public IQueryable<tblLocation> GettblLocations()
        {
            return db.tblLocations;
        }

        // GET: api/tblLocations/5
        [ResponseType(typeof(tblLocation))]
        public IHttpActionResult GettblLocations(int id)
        {
            var tblLocations = db.GetByIdLocations(id);
            if (tblLocations == null)
            {
                return NotFound();
            }

            return Ok(tblLocations);
        }

        // PUT: api/tblLocations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblLocations(int id, tblLocation tblLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblLocations.LocationID)
            {
                return BadRequest();
            }

            db.Entry(tblLocations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblLocationsExists(id))
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

        // POST: api/tblLocations
        [ResponseType(typeof(tblLocation))]
        public IHttpActionResult PosttblLocations(tblLocation tblLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblLocations(tblLocations.CurrentLication, tblLocations.CountryID, tblLocations.CityID, tblLocations.NeighbourhoodID, tblLocations.Title, false);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblLocations.LocationID }, tblLocations);
        }

        // DELETE: api/tblLocations/5
        [ResponseType(typeof(tblLocation))]
        public IHttpActionResult DeletetblLocations(int id)
        {

            db.sp_DeltblLocations(id);
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

        private bool tblLocationsExists(int id)
        {
            return db.tblLocations.Count(e => e.LocationID == id) > 0;
        }
    }
}