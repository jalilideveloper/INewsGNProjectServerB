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
    public class tblBlockTagsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblBlockTags
        public IQueryable<tblBlockTag> GettblBlockTag()
        {
            return db.tblBlockTags;
        }

        // GET: api/tblBlockTags/5
        [ResponseType(typeof(tblBlockTag))]
        public IHttpActionResult GettblBlockTag(int id)
        {
            var tblBlockTag = db.GetByIdBlockTag(id);
            if (tblBlockTag == null)
            {
                return NotFound();
            }

            return Ok(tblBlockTag);
        }

        // PUT: api/tblBlockTags/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblBlockTag(int id, tblBlockTag tblBlockTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblBlockTag.BlockKeyID)
            {
                return BadRequest();
            }

            db.Entry(tblBlockTag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblBlockTagExists(id))
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

        // POST: api/tblBlockTags
        [ResponseType(typeof(tblBlockTag))]
        public IHttpActionResult PosttblBlockTag(tblBlockTag tblBlockTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblBlockTag(tblBlockTag.BlockKeywords,false);
           
            return CreatedAtRoute("DefaultApi", new { id = tblBlockTag.BlockKeyID }, tblBlockTag);
        }

        // DELETE: api/tblBlockTags/5
        [ResponseType(typeof(tblBlockTag))]
        public IHttpActionResult DeletetblBlockTag(int id)
        {
            db.sp_DeltblBlockTag(id);
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

        private bool tblBlockTagExists(int id)
        {
            return db.tblBlockTags.Count(e => e.BlockKeyID == id) > 0;
        }
    }
}