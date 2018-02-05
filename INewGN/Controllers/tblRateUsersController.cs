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
    public class tblRateUsersController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblRateUsers
        public IQueryable<tblRateUser> GettblRateUsers()
        {
            return db.tblRateUsers;
        }

        // GET: api/tblRateUsers/5
        [ResponseType(typeof(tblRateUser))]
        public IHttpActionResult GettblRateUsers(int id)
        {
            var tblRateUsers = db.GetByIdRateUsers(id);
            if (tblRateUsers == null)
            {
                return NotFound();
            }

            return Ok(tblRateUsers);
        }

        // PUT: api/tblRateUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblRateUsers(int id, tblRateUser tblRateUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblRateUsers.UserRateID)
            {
                return BadRequest();
            }

            db.Entry(tblRateUsers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblRateUsersExists(id))
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

        // POST: api/tblRateUsers
        [ResponseType(typeof(tblRateUser))]
        public IHttpActionResult PosttblRateUsers(tblRateUser tblRateUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblRateUsers(tblRateUsers.UserID, tblRateUsers.PostCount, tblRateUsers.LikesCount, tblRateUsers.UnLikesCount, tblRateUsers.CommentsCount, tblRateUsers.SaveImageCount, tblRateUsers.ShareFileCount, tblRateUsers.FriendCount, tblRateUsers.SpamCount, false);
   

            return CreatedAtRoute("DefaultApi", new { id = tblRateUsers.UserRateID }, tblRateUsers);
        }

        // DELETE: api/tblRateUsers/5
        [ResponseType(typeof(tblRateUser))]
        public IHttpActionResult DeletetblRateUsers(int id)
        {
            db.sp_DeltblRateUsers(id);
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

        private bool tblRateUsersExists(int id)
        {
            return db.tblRateUsers.Count(e => e.UserRateID == id) > 0;
        }
    }
}