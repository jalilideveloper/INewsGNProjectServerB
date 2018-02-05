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
    public class tblLanguagesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblLanguages
        public IQueryable<tblLanguage> GettblLanguages()
        {
            return db.tblLanguages;
        }

        // GET: api/tblLanguages/5
        [ResponseType(typeof(tblLanguage))]
        public IHttpActionResult GettblLanguages(int id)
        {
            var tblLanguages = db.GetByIdLanguages(id);
            if (tblLanguages == null)
            {
                return NotFound();
            }

            return Ok(tblLanguages);
        }

        // PUT: api/tblLanguages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblLanguages(int id, tblLanguage tblLanguages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblLanguages.LanguageID)
            {
                return BadRequest();
            }

            db.Entry(tblLanguages).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblLanguagesExists(id))
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

        // POST: api/tblLanguages
        [ResponseType(typeof(tblLanguage))]
        public IHttpActionResult PosttblLanguages(tblLanguage tblLanguages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblLanguages(tblLanguages.Lnaguage , tblLanguages.IsDeleted);
            
            return CreatedAtRoute("DefaultApi", new { id = tblLanguages.LanguageID }, tblLanguages);
        }

        // DELETE: api/tblLanguages/5
        [ResponseType(typeof(tblLanguage))]
        public IHttpActionResult DeletetblLanguages(int id)
        {

            db.sp_DeltblLanguages(id);
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

        private bool tblLanguagesExists(int id)
        {
            return db.tblLanguages.Count(e => e.LanguageID == id) > 0;
        }
    }
}