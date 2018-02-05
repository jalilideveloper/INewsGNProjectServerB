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
    public class tblPageFreindsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblPageFreinds
        public IQueryable<tblPageFreind> GettblPageFreinds()
        {
            return db.tblPageFreinds;
        }

        // GET: api/tblPageFreinds/5
        [ResponseType(typeof(tblPageFreind))]
        public IHttpActionResult GettblPageFreinds(int id)
        {
            var tblPageFreinds = db.GetByIdPageFreinds(id);
            if (tblPageFreinds == null)
            {
                return NotFound();
            }

            return Ok(tblPageFreinds);
        }

        // PUT: api/tblPageFreinds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblPageFreinds(int id, tblPageFreind tblPageFreinds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblPageFreinds.PageFreindID)
            {
                return BadRequest();
            }

            db.Entry(tblPageFreinds).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblPageFreindsExists(id))
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

        // POST: api/tblPageFreinds
        [ResponseType(typeof(tblPageFreind))]
        public IHttpActionResult PosttblPageFreinds(tblPageFreind tblPageFreinds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblPageFreinds(tblPageFreinds.UserWallPageID, tblPageFreinds.UserID, tblPageFreinds.Invited, tblPageFreinds.Confrim, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblPageFreinds.PageFreindID }, tblPageFreinds);
        }

        // DELETE: api/tblPageFreinds/5
        [ResponseType(typeof(tblPageFreind))]
        public IHttpActionResult DeletetblPageFreinds(int id)
        {
            db.sp_DeltblPageFreinds(id);
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

        private bool tblPageFreindsExists(int id)
        {
            return db.tblPageFreinds.Count(e => e.PageFreindID == id) > 0;
        }
    }
}