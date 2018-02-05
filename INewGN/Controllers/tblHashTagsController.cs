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
    public class tblHashTagsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblHashTags
        public IQueryable<tblHashTag> GettblHashTag()
        {
            return db.tblHashTags;
        }

        // GET: api/tblHashTags/5
        [ResponseType(typeof(tblHashTag))]
        public IHttpActionResult GettblHashTag(string id)
        {
            string [] arr = id.Split('-');
            if (arr[0] == "GetTop50NewsByTag")
            {
                var tblHashTag = db.GetLatestNewsByTagNameOrderByDate(Convert.ToInt32(arr[3]), Convert.ToInt32(arr[2]), arr[1],0,false);
                if (tblHashTag == null)
                {
                    return NotFound();
                }
                return Ok(tblHashTag);
            }
            else if (arr[0] == "GetTop50NewsByTagWithCategory")
            {
                var tblHashTag = db.GetLatestNewsByTagNameOrderByDate(Convert.ToInt32(arr[4]), Convert.ToInt32(arr[3]), arr[2],Convert.ToInt32(arr[1]), true);
                if (tblHashTag == null)
                {
                    return NotFound();
                }
                return Ok(tblHashTag);
            }
            

            else if (arr[0] == "GetTagTop10DWMY")
            {
                var tblHashTag = db.GetTop200HashTagDayWeekMonthYear(arr[1]);
                if (tblHashTag == null)
                {
                    return NotFound();
                }
                return Ok(tblHashTag);
            }
           
            else
            {
                var tblHashTag = db.GetByIdHashTag(Convert.ToInt32(arr[1]));
                if (tblHashTag == null)
                {
                    return NotFound();
                }
                return Ok(tblHashTag);

            }
        }

        // PUT: api/tblHashTags/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblHashTag(int id, tblHashTag tblHashTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblHashTag.HashtagID)
            {
                return BadRequest();
            }

            db.Entry(tblHashTag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblHashTagExists(id))
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

        // POST: api/tblHashTags
        [ResponseType(typeof(tblHashTag))]
        public IHttpActionResult PosttblHashTag(tblHashTag tblHashTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblHashTag(tblHashTag.HashtagName,false,0,1,1,0);
            
            return CreatedAtRoute("DefaultApi", new { id = tblHashTag.HashtagID }, tblHashTag);
        }

        // DELETE: api/tblHashTags/5
        [ResponseType(typeof(tblHashTag))]
        public IHttpActionResult DeletetblHashTag(int id)
        {

            db.sp_DeltblHashTag(id);
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

        private bool tblHashTagExists(int id)
        {
            return db.tblHashTags.Count(e => e.HashtagID == id) > 0;
        }
    }
}