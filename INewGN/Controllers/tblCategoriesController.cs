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
using System.Web.Script.Serialization;

namespace INewGN.Controllers
{
    public class tblCategoriesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblCategories
        public IHttpActionResult GettblCategories()
        {

            return Ok(db.GetAllCategories(1));
        }

        // GET: api/tblCategories/5
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult GettblCategories(string id)
        {

            string[] arr = id.Split('-');
            var tblCategories = db.GetByIdCategories(Convert.ToInt32(arr[1]), Convert.ToInt32(arr[0]));
            if (tblCategories == null)
            {
                return NotFound();
            }

            return Ok(tblCategories);
        }

        // PUT: api/tblCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCategories(int id, tblCategory tblCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCategories.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(tblCategories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCategoriesExists(id))
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

        // POST: api/tblCategories
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult PosttblCategories(tblCategory tblCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblCategories(tblCategories.LanguageID, tblCategories.CategoryName, tblCategories.ParrentID, false);


            return CreatedAtRoute("DefaultApi", new { id = tblCategories.CategoryID }, tblCategories);
        }

        // DELETE: api/tblCategories/5
        [ResponseType(typeof(tblCategory))]
        public IHttpActionResult DeletetblCategories(int id)
        {
            db.sp_DeltblCategories(id);
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

        private bool tblCategoriesExists(int id)
        {
            return db.tblCategories.Count(e => e.CategoryID == id) > 0;
        }
    }
}