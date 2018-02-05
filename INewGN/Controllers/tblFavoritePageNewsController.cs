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
    public class tblFavoritePageNewsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblFavoritePageNews
        public IQueryable<tblFavoritePageNew> GettblFavoritePageNews()
        {
            return db.tblFavoritePageNews;
        }

        // GET: api/tblFavoritePageNews/5
        [ResponseType(typeof(tblFavoritePageNew))]
        public IHttpActionResult GettblFavoritePageNews(int id)
        {
            var tblFavoritePageNews = db.GetByIdFavoritePageNews(id);
            if (tblFavoritePageNews == null)
            {
                return NotFound();
            }

            return Ok(tblFavoritePageNews);
        }

        // PUT: api/tblFavoritePageNews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblFavoritePageNews(int id, tblFavoritePageNew tblFavoritePageNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblFavoritePageNews.FavoritesCategoryNewsID)
            {
                return BadRequest();
            }

            db.Entry(tblFavoritePageNews).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblFavoritePageNewsExists(id))
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

        // POST: api/tblFavoritePageNews
        [ResponseType(typeof(tblFavoritePageNew))]
        public IHttpActionResult PosttblFavoritePageNews(tblFavoritePageNew tblFavoritePageNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblFavoritePageNews(tblFavoritePageNews.FavoritesCategoryNewsID, tblFavoritePageNews.UserWallPageID, tblFavoritePageNews.CategoryIDNews,false);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblFavoritePageNewsExists(tblFavoritePageNews.FavoritesCategoryNewsID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblFavoritePageNews.FavoritesCategoryNewsID }, tblFavoritePageNews);
        }

        // DELETE: api/tblFavoritePageNews/5
        [ResponseType(typeof(tblFavoritePageNew))]
        public IHttpActionResult DeletetblFavoritePageNews(int id)
        {

            db.sp_DeltblFavoritePageNews(id);
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

        private bool tblFavoritePageNewsExists(int id)
        {
            return db.tblFavoritePageNews.Count(e => e.FavoritesCategoryNewsID == id) > 0;
        }
    }
}