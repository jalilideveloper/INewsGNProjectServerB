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
    public class tblTimesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblTimes
        public IQueryable<tblTime> GettblTimes()
        {
            return db.tblTimes;
        }

        // GET: api/tblTimes/5
        [ResponseType(typeof(tblTime))]
        public IHttpActionResult GettblTimes(int id)
        {
            var tblTimes = db.GetByIdTimes(id);
            if (tblTimes == null)
            {
                return NotFound();
            }

            return Ok(tblTimes);
        }

        // PUT: api/tblTimes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblTimes(int id, tblTime tblTimes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblTimes.TimeID)
            {
                return BadRequest();
            }

            db.Entry(tblTimes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTimesExists(id))
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

        // POST: api/tblTimes
        [ResponseType(typeof(tblTime))]
        public IHttpActionResult PosttblTimes(tblTime tblTimes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblTimes(tblTimes.StartTime, tblTimes.EndTime, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblTimes.TimeID }, tblTimes);
        }

        // DELETE: api/tblTimes/5
        [ResponseType(typeof(tblTime))]
        public IHttpActionResult DeletetblTimes(int id)
        {
            db.sp_DeltblTimes(id);
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

        private bool tblTimesExists(int id)
        {
            return db.tblTimes.Count(e => e.TimeID == id) > 0;
        }
    }
}