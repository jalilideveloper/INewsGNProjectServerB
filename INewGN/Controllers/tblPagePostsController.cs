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
    public class tblPagePostsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblPagePosts
        public IQueryable<tblPagePost> GettblPagePost()
        {
            return db.tblPagePosts;
        }

        // GET: api/tblPagePosts/5
        [ResponseType(typeof(tblPagePost))]
        public IHttpActionResult GettblPagePost(int id)
        {
            tblPagePost tblPagePost = db.tblPagePosts.Find(id);
            if (tblPagePost == null)
            {
                return NotFound();
            }

            return Ok(tblPagePost);
        }

        // PUT: api/tblPagePosts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblPagePost(int id, tblPagePost tblPagePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblPagePost.PagePostId)
            {
                return BadRequest();
            }

            db.Entry(tblPagePost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblPagePostExists(id))
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

        // POST: api/tblPagePosts
        [ResponseType(typeof(tblPagePost))]
        public IHttpActionResult PosttblPagePost(tblPagePost tblPagePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblPagePost(tblPagePost.UserWallPageID, tblPagePost.Title, tblPagePost.Description, tblPagePost.UnLike, tblPagePost.Likes, tblPagePost.Visit, tblPagePost.CategoryID, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblPagePost.PagePostId }, tblPagePost);
        }

        // DELETE: api/tblPagePosts/5
        [ResponseType(typeof(tblPagePost))]
        public IHttpActionResult DeletetblPagePost(int id)
        {
            db.sp_DeltblPagePost(id);
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

        private bool tblPagePostExists(int id)
        {
            return db.tblPagePosts.Count(e => e.PagePostId == id) > 0;
        }
    }
}