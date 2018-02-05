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
    public class tblDaysController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblDays
        public IQueryable<tblDay> GettblDays()
        {
            return db.tblDays;
        }

        // GET: api/tblDays/5
        [ResponseType(typeof(tblDay))]
        public IHttpActionResult GettblDays(int id)
        {
            var tblDays = db.GetByIdDays(id);
            if (tblDays == null)
            {
                return NotFound();
            }

            return Ok(tblDays);
        }

        // PUT: api/tblDays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblDays(int id, tblDay tblDays)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblDays.DayID)
            {
                return BadRequest();
            }

            db.Entry(tblDays).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDaysExists(id))
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

        // POST: api/tblDays
        [ResponseType(typeof(tblDay))]
        public IHttpActionResult PosttblDays(tblDay tblDays)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblDays(tblDays.DayName,tblDays.LanguageID,tblDays.IsDeleted);
            

            return CreatedAtRoute("DefaultApi", new { id = tblDays.DayID }, tblDays);
        }

        // DELETE: api/tblDays/5
        [ResponseType(typeof(tblDay))]
        public IHttpActionResult DeletetblDays(int id)
        {

            db.sp_DeltblDays(id);
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

        private bool tblDaysExists(int id)
        {
            return db.tblDays.Count(e => e.DayID == id) > 0;
        }
    }
}