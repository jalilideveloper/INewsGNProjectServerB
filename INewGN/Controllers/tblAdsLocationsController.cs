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
    public class tblAdsLocationsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblAdsLocations
        public IQueryable<tblAdsLocation> GettblAdsLocations()
        {
            return db.tblAdsLocations;
        }

        // GET: api/tblAdsLocations/5
        [ResponseType(typeof(tblAdsLocation))]
        public IHttpActionResult GettblAdsLocations(int id)
        {
            var tblAdsLocations = db.GetByIdAdsLocation(id);
            if (tblAdsLocations == null)
            {
                return NotFound();
            }

            return Ok(tblAdsLocations);
        }

        // PUT: api/tblAdsLocations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblAdsLocations(int id, tblAdsLocation tblAdsLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblAdsLocations.AdsLocID)
            {
                return BadRequest();
            }

            db.Entry(tblAdsLocations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblAdsLocationsExists(id))
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

        // POST: api/tblAdsLocations
        [ResponseType(typeof(tblAdsLocation))]
        public IHttpActionResult PosttblAdsLocations(tblAdsLocation tblAdsLocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int Result = db.sp_Add_tblAdsLocations(tblAdsLocations.LocationID,tblAdsLocations.AdsID, false);
            

            return CreatedAtRoute("DefaultApi", new { id = tblAdsLocations.AdsLocID }, tblAdsLocations);
        }

        // DELETE: api/tblAdsLocations/5
        [ResponseType(typeof(tblAdsLocation))]
        public IHttpActionResult DeletetblAdsLocations(int id)
        {
            
            db.sp_DeltblAdsLocations(id);
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

        private bool tblAdsLocationsExists(int id)
        {
            return db.tblAdsLocations.Count(e => e.AdsLocID == id) > 0;
        }
    }
}