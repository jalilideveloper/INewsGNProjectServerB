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
    public class tblLinkestansController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblLinkestans
        public IHttpActionResult GettblLinkestan()
        {
            var tblLinkestan = db.GetAllLinkestans(1);
            if (tblLinkestan == null)
            {
                return NotFound();
            }

            return Ok(tblLinkestan);
        }

        // GET: api/tblLinkestans/5
        [ResponseType(typeof(tblLinkestan))]
        public IHttpActionResult GettblLinkestan(string id)
        {
            string[] arr = id.Split('-');
            if (arr[0] == "All")
            {

                var tblLinkestan = db.GetAllLinkestans(Convert.ToInt32(arr[1]));
                if (tblLinkestan == null)
                {
                    return NotFound();
                }

                return Ok(tblLinkestan);
            }
            else 
            {
                var tblLinkestan = db.GetByIdLinkestan(Convert.ToInt32(arr[0]));
                if (tblLinkestan == null)
                {
                    return NotFound();
                }
                return Ok(tblLinkestan);
            }
           
        }

        // PUT: api/tblLinkestans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblLinkestan(int id, tblLinkestan tblLinkestan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblLinkestan.LikestanID)
            {
                return BadRequest();
            }

            db.Entry(tblLinkestan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblLinkestanExists(id))
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

        // POST: api/tblLinkestans
        [ResponseType(typeof(tblLinkestan))]
        public IHttpActionResult PosttblLinkestan(tblLinkestan tblLinkestan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblLinkestan(tblLinkestan.UserID, tblLinkestan.CategoryID, tblLinkestan.LinkName, tblLinkestan.Email, tblLinkestan.Description, false, tblLinkestan.LanguageID,tblLinkestan.Title,tblLinkestan.Keywords,DateTime.Now.Date);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblLinkestan.LikestanID }, tblLinkestan);
        }

        // DELETE: api/tblLinkestans/5
        [ResponseType(typeof(tblLinkestan))]
        public IHttpActionResult DeletetblLinkestan(int id)
        {

            db.sp_DeltblLinkestan(id);
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

        private bool tblLinkestanExists(int id)
        {
            return db.tblLinkestans.Count(e => e.LikestanID == id) > 0;
        }
    }
}