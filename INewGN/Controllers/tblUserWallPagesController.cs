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
    public class tblUserWallPagesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblUserWallPages
        public IQueryable<tblUserWallPage> GettblUserWallPages()
        {
            return db.tblUserWallPages;
        }

        // GET: api/tblUserWallPages/5
        [ResponseType(typeof(tblUserWallPage))]
        public IHttpActionResult GettblUserWallPages(string id)
        {
            string[] arr = id.Split('-');
            if (arr[1] == "Multi")
            {
                var tblUserWallPages = db.GetAllWallPagesByUserID(Convert.ToInt32(arr[0]));
                if (tblUserWallPages == null)
                {
                    return NotFound();
                }

                return Ok(tblUserWallPages);
            }
            else
            {
                var tblUserWallPages = db.GetByIdUserWallPages(Convert.ToInt32(arr[0]));
                if (tblUserWallPages == null)
                {
                    return NotFound();
                }

                return Ok(tblUserWallPages);
            }
          
        }

        // PUT: api/tblUserWallPages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUserWallPages(int id, tblUserWallPage tblUserWallPages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUserWallPages.UserWallPageID)
            {
                return BadRequest();
            }

            db.Entry(tblUserWallPages).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserWallPagesExists(id))
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

        // POST: api/tblUserWallPages
        [ResponseType(typeof(tblUserWallPage))]
        public IHttpActionResult PosttblUserWallPages(tblUserWallPage tblUserWallPages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblUserWallPages(tblUserWallPages.UserID, tblUserWallPages.PrivacyTypeID, tblUserWallPages.PageRate, tblUserWallPages.Block, tblUserWallPages.Confrim, tblUserWallPages.RegisterDate, tblUserWallPages.Title, tblUserWallPages.TelegramLink, tblUserWallPages.FacebookLink, tblUserWallPages.TwitterLink, tblUserWallPages.GooglePlusLink, tblUserWallPages.Description, tblUserWallPages.FriendCount, tblUserWallPages.PageImageUrl, tblUserWallPages.LocationID, false);
            

            return CreatedAtRoute("DefaultApi", new { id = tblUserWallPages.UserWallPageID }, tblUserWallPages);
        }

        // DELETE: api/tblUserWallPages/5
        [ResponseType(typeof(tblUserWallPage))]
        public IHttpActionResult DeletetblUserWallPages(int id)
        {
            db.sp_DeltblUserWallPages(id);
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

        private bool tblUserWallPagesExists(int id)
        {
            return db.tblUserWallPages.Count(e => e.UserWallPageID == id) > 0;
        }
    }
}