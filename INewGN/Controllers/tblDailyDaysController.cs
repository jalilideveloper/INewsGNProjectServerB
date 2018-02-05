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
    public class tblDailyDaysController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblDailyDays
        public IQueryable<tblDailyDay> GettblDailyDays()
        {
            return db.tblDailyDays;
        }

        // GET: api/tblDailyDays/5
        [ResponseType(typeof(tblDailyDay))]
        public IHttpActionResult GettblDailyDays(int id)
        {
            var tblDailyDays = db.GetByIdDailyDays(id);
            if (tblDailyDays == null)
            {
                return NotFound();
            }

            return Ok(tblDailyDays);
        }

        // PUT: api/tblDailyDays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblDailyDays(int id, tblDailyDay tblDailyDays)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblDailyDays.DailyDaysID)
            {
                return BadRequest();
            }

            db.Entry(tblDailyDays).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDailyDaysExists(id))
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

        // POST: api/tblDailyDays
        [ResponseType(typeof(tblDailyDay))]
        public IHttpActionResult PosttblDailyDays(tblDailyDay tblDailyDays)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.sp_Add_tblDailyDays(tblDailyDays.DayID,tblDailyDays.TimeID,tblDailyDays.AdsShedualsID,false);
           

            return CreatedAtRoute("DefaultApi", new { id = tblDailyDays.DailyDaysID }, tblDailyDays);
        }

        // DELETE: api/tblDailyDays/5
        [ResponseType(typeof(tblDailyDay))]
        public IHttpActionResult DeletetblDailyDays(int id)
        {
            db.sp_DeltblDailyDays(id);
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

        private bool tblDailyDaysExists(int id)
        {
            return db.tblDailyDays.Count(e => e.DailyDaysID == id) > 0;
        }
    }
}