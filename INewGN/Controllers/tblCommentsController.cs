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
    public class tblCommentsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblComments
        public IQueryable<tblComment> GettblComments()
        {
            return db.tblComments;
        }

        // GET: api/tblComments/5
        [ResponseType(typeof(tblComment))]
        public IHttpActionResult GettblComments(int id)
        {
            var tblComments = db.GetByIdComments(id);
            if (tblComments == null)
            {
                return NotFound();
            }

            return Ok(tblComments);
        }

        // PUT: api/tblComments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblComments(int id, tblComment tblComments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblComments.CommentID)
            {
                return BadRequest();
            }

            db.Entry(tblComments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCommentsExists(id))
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

        // POST: api/tblComments
        [ResponseType(typeof(tblComment))]
        public IHttpActionResult PosttblComments(tblComment tblComments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblComments(tblComments.LanguageID,tblComments.Comments,tblComments.UserID,tblComments.NewsID,false);
            

            return CreatedAtRoute("DefaultApi", new { id = tblComments.CommentID }, tblComments);
        }

        // DELETE: api/tblComments/5
        [ResponseType(typeof(tblComment))]
        public IHttpActionResult DeletetblComments(int id)
        {
            db.sp_DeltblComments(id);
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

        private bool tblCommentsExists(int id)
        {
            return db.tblComments.Count(e => e.CommentID == id) > 0;
        }
    }
}