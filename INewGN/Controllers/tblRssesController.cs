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
    public class tblRssesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblRsses
        public IQueryable<tblRss> GettblRss()
        {
            return db.tblRsses;
        }

        // GET: api/tblRsses/5
        [ResponseType(typeof(tblRss))]
        public IHttpActionResult GettblRss(int id)
        {
            var tblRss = db.GetByIdRss(id);
            if (tblRss == null)
            {
                return NotFound();
            }

            return Ok(tblRss);
        }

        // PUT: api/tblRsses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblRss(int id, tblRss tblRss)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblRss.RssID)
            {
                return BadRequest();
            }

            db.Entry(tblRss).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblRssExists(id))
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

        // POST: api/tblRsses
        [ResponseType(typeof(tblRss))]
        public IHttpActionResult PosttblRss(tblRss tblRss)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblRss(tblRss.RssUrl, tblRss.Title, tblRss.Description, DateTime.Now.Date, tblRss.CategoryID, tblRss.LanguageID, false);
            

            return CreatedAtRoute("DefaultApi", new { id = tblRss.RssID }, tblRss);
        }

        // DELETE: api/tblRsses/5
        [ResponseType(typeof(tblRss))]
        public IHttpActionResult DeletetblRss(int id)
        {
            db.sp_DeltblRss(id);
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

        private bool tblRssExists(int id)
        {
            return db.tblRsses.Count(e => e.RssID == id) > 0;
        }
    }
}