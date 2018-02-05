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
    public class tblPrivacyTypesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblPrivacyTypes
        public IQueryable<tblPrivacyType> GettblPrivacyType()
        {
            return db.tblPrivacyTypes;
        }

        // GET: api/tblPrivacyTypes/5
        [ResponseType(typeof(tblPrivacyType))]
        public IHttpActionResult GettblPrivacyType(int id)
        {
            var tblPrivacyType = db.GetByIdPagePrivacyType(id);
            if (tblPrivacyType == null)
            {
                return NotFound();
            }

            return Ok(tblPrivacyType);
        }

        // PUT: api/tblPrivacyTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblPrivacyType(int id, tblPrivacyType tblPrivacyType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblPrivacyType.PrivacyTypeID)
            {
                return BadRequest();
            }

            db.Entry(tblPrivacyType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblPrivacyTypeExists(id))
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

        // POST: api/tblPrivacyTypes
        [ResponseType(typeof(tblPrivacyType))]
        public IHttpActionResult PosttblPrivacyType(tblPrivacyType tblPrivacyType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblPrivacyType(tblPrivacyType.PrivacyType, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblPrivacyType.PrivacyTypeID }, tblPrivacyType);
        }

        // DELETE: api/tblPrivacyTypes/5
        [ResponseType(typeof(tblPrivacyType))]
        public IHttpActionResult DeletetblPrivacyType(int id)
        {
            db.sp_DeltblPrivacyType(id);
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

        private bool tblPrivacyTypeExists(int id)
        {
            return db.tblPrivacyTypes.Count(e => e.PrivacyTypeID == id) > 0;
        }
    }
}