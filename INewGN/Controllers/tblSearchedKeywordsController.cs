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
    public class tblSearchedKeywordsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblSearchedKeywords
        public IQueryable<tblSearchedKeyword> GettblSearchedKeyword()
        {
            return db.tblSearchedKeywords;
        }

        // GET: api/tblSearchedKeywords/5
        [ResponseType(typeof(tblSearchedKeyword))]
        public IHttpActionResult GettblSearchedKeyword(int id)
        {
            var tblSearchedKeyword = db.GetByIdSearchedKeyword(id);
            if (tblSearchedKeyword == null)
            {
                return NotFound();
            }

            return Ok(tblSearchedKeyword);
        }

        // PUT: api/tblSearchedKeywords/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblSearchedKeyword(int id, tblSearchedKeyword tblSearchedKeyword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblSearchedKeyword.SearchedKeyID)
            {
                return BadRequest();
            }

            db.Entry(tblSearchedKeyword).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblSearchedKeywordExists(id))
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

        // POST: api/tblSearchedKeywords
        [ResponseType(typeof(tblSearchedKeyword))]
        public IHttpActionResult PosttblSearchedKeyword(tblSearchedKeyword tblSearchedKeyword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblSearchedKeyword(tblSearchedKeyword.SearchedKeyword, tblSearchedKeyword.CountSearched,DateTime.Now.Date,DateTime.Now.Date ,tblSearchedKeyword.LastUpdateInMount, tblSearchedKeyword.LocationID, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblSearchedKeyword.SearchedKeyID }, tblSearchedKeyword);
        }

        // DELETE: api/tblSearchedKeywords/5
        [ResponseType(typeof(tblSearchedKeyword))]
        public IHttpActionResult DeletetblSearchedKeyword(int id)
        {
            db.sp_DeltblSearchedKeyword(id);
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

        private bool tblSearchedKeywordExists(int id)
        {
            return db.tblSearchedKeywords.Count(e => e.SearchedKeyID == id) > 0;
        }
    }
}