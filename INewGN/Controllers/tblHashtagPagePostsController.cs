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
    public class tblHashtagPagePostsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblHashtagPagePosts
        public IQueryable<tblHashtagPagePost> GettblHashtagPagePost()
        {
            return db.tblHashtagPagePosts;
        }

        // GET: api/tblHashtagPagePosts/5
        [ResponseType(typeof(tblHashtagPagePost))]
        public IHttpActionResult GettblHashtagPagePost(int id)
        {
            var tblHashtagPagePost = db.GetByIdHashtagPagePost(id);
            if (tblHashtagPagePost == null)
            {
                return NotFound();
            }

            return Ok(tblHashtagPagePost);
        }

        // PUT: api/tblHashtagPagePosts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblHashtagPagePost(int id, tblHashtagPagePost tblHashtagPagePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblHashtagPagePost.HashtagPagePostID)
            {
                return BadRequest();
            }

            db.Entry(tblHashtagPagePost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblHashtagPagePostExists(id))
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

        // POST: api/tblHashtagPagePosts
        [ResponseType(typeof(tblHashtagPagePost))]
        public IHttpActionResult PosttblHashtagPagePost(tblHashtagPagePost tblHashtagPagePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblHashtagPagePost(tblHashtagPagePost.PagePostID, tblHashtagPagePost.HashtagID, false);
            
            return CreatedAtRoute("DefaultApi", new { id = tblHashtagPagePost.HashtagPagePostID }, tblHashtagPagePost);
        }

        // DELETE: api/tblHashtagPagePosts/5
        [ResponseType(typeof(tblHashtagPagePost))]
        public IHttpActionResult DeletetblHashtagPagePost(int id)
        {

            db.sp_DeltblHashtagPagePost(id);
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

        private bool tblHashtagPagePostExists(int id)
        {
            return db.tblHashtagPagePosts.Count(e => e.HashtagPagePostID == id) > 0;
        }
    }
}