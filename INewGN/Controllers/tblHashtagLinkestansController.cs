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
    public class tblHashtagLinkestansController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblHashtagLinkestans
        public IQueryable<tblHashtagLinkestan> GettblHashtagLinkestan()
        {
            return db.tblHashtagLinkestans;
        }

        // GET: api/tblHashtagLinkestans/5
        [ResponseType(typeof(tblHashtagLinkestan))]
        public IHttpActionResult GettblHashtagLinkestan(int id)
        {
            var tblHashtagLinkestan = db.GetByIdHashtagLinkestan(id);
            if (tblHashtagLinkestan == null)
            {
                return NotFound();
            }

            return Ok(tblHashtagLinkestan);
        }

        // PUT: api/tblHashtagLinkestans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblHashtagLinkestan(int id, tblHashtagLinkestan tblHashtagLinkestan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblHashtagLinkestan.HtagLinkID)
            {
                return BadRequest();
            }

            db.Entry(tblHashtagLinkestan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblHashtagLinkestanExists(id))
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

        // POST: api/tblHashtagLinkestans
        [ResponseType(typeof(tblHashtagLinkestan))]
        public IHttpActionResult PosttblHashtagLinkestan(tblHashtagLinkestan tblHashtagLinkestan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblHashtagLinkestan(tblHashtagLinkestan.LinkestanID, tblHashtagLinkestan.HashtagID, tblHashtagLinkestan.IsDeleted, tblHashtagLinkestan.LanguageID);
            
            return CreatedAtRoute("DefaultApi", new { id = tblHashtagLinkestan.HtagLinkID }, tblHashtagLinkestan);
        }

        // DELETE: api/tblHashtagLinkestans/5
        [ResponseType(typeof(tblHashtagLinkestan))]
        public IHttpActionResult DeletetblHashtagLinkestan(int id)
        {

            db.sp_DeltblHashtagLinkestan(id);
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

        private bool tblHashtagLinkestanExists(int id)
        {
            return db.tblHashtagLinkestans.Count(e => e.HtagLinkID == id) > 0;
        }
    }
}