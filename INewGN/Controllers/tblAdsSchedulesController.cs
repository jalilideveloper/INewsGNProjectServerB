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
    public class tblAdsSchedulesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblAdsSchedules
        public IQueryable<tblAdsSchedule> GettblAdsSchedules()
        {
            return db.tblAdsSchedules;
        }

        // GET: api/tblAdsSchedules/5
        [ResponseType(typeof(tblAdsSchedule))]
        public IHttpActionResult GettblAdsSchedules(int id)
        {
            //var tblAdsSchedules = db.GetByIdAdsSchedules(id);

            var tblAdsSchedules = "";
            if (tblAdsSchedules == null)
            {
                return NotFound();
            }

            return Ok(tblAdsSchedules);
        }

        // PUT: api/tblAdsSchedules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblAdsSchedules(int id, tblAdsSchedule tblAdsSchedules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblAdsSchedules.AdsShedualsID)
            {
                return BadRequest();
            }

            db.Entry(tblAdsSchedules).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblAdsSchedulesExists(id))
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

        // POST: api/tblAdsSchedules
        [ResponseType(typeof(tblAdsSchedule))]
        public IHttpActionResult PosttblAdsSchedules(tblAdsSchedule tblAdsSchedules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           int result =  db.sp_Add_tblAdsSchedules(tblAdsSchedules.UserID,tblAdsSchedules.StartDate, tblAdsSchedules.EndDate, tblAdsSchedules.DailyDaysID, tblAdsSchedules.UploadAdsFileID, tblAdsSchedules.RegisterDate, false,false,false);
           

            return CreatedAtRoute("DefaultApi", new { id = tblAdsSchedules.AdsShedualsID }, tblAdsSchedules);
        }

        // DELETE: api/tblAdsSchedules/5
        [ResponseType(typeof(tblAdsSchedule))]
        public IHttpActionResult DeletetblAdsSchedules(int id)
        {
            //db.sp_DeltblAdsSchedules(id);
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

        private bool tblAdsSchedulesExists(int id)
        {
            return db.tblAdsSchedules.Count(e => e.AdsShedualsID == id) > 0;
        }
    }
}