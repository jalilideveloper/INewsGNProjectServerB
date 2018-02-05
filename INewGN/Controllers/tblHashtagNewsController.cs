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
    public class tblHashtagNewsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblHashtagNews
        public IQueryable<tblHashtagNew> GettblHashtagNews()
        {
            return db.tblHashtagNews;
        }

        // GET: api/tblHashtagNews/5
        [ResponseType(typeof(tblHashtagNew))]
        public IHttpActionResult GettblHashtagNews(int id)
        {
            var tblHashtagNews = db.GetByIdHashtagNews(id);
            if (tblHashtagNews == null)
            {
                return NotFound();
            }

            return Ok(tblHashtagNews);
        }

        // PUT: api/tblHashtagNews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblHashtagNews(int id, tblHashtagNew tblHashtagNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblHashtagNews.HashTagNewsID)
            {
                return BadRequest();
            }

            db.Entry(tblHashtagNews).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblHashtagNewsExists(id))
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

        // POST: api/tblHashtagNews
        [ResponseType(typeof(tblHashtagNew))]
        public IHttpActionResult PosttblHashtagNews(tblHashtagNew tblHashtagNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblHashtagNews(tblHashtagNews.NewsID, tblHashtagNews.HashtagID,  false, tblHashtagNews.LanguageID);
            
            return CreatedAtRoute("DefaultApi", new { id = tblHashtagNews.HashTagNewsID }, tblHashtagNews);
        }

        // DELETE: api/tblHashtagNews/5
        [ResponseType(typeof(tblHashtagNew))]
        public IHttpActionResult DeletetblHashtagNews(int id)
        {

            db.sp_DeltblHashtagNews(id);
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

        private bool tblHashtagNewsExists(int id)
        {
            return db.tblHashtagNews.Count(e => e.HashTagNewsID == id) > 0;
        }
    }
}