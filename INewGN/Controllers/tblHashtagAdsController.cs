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
    public class tblHashtagAdsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblHashtagAds
        public IQueryable<tblHashtagAd> GettblHashtagAds()
        {
            return db.tblHashtagAds;
        }

        // GET: api/tblHashtagAds/5
        [ResponseType(typeof(tblHashtagAd))]
        public IHttpActionResult GettblHashtagAds(int id)
        {
            var tblHashtagAds = db.GetByIdHashtagAds(id);
            if (tblHashtagAds == null)
            {
                return NotFound();
            }

            return Ok(tblHashtagAds);
        }

        // PUT: api/tblHashtagAds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblHashtagAds(int id, tblHashtagAd tblHashtagAds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblHashtagAds.HashtagAdsID)
            {
                return BadRequest();
            }

            db.Entry(tblHashtagAds).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblHashtagAdsExists(id))
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

        // POST: api/tblHashtagAds
        [ResponseType(typeof(tblHashtagAd))]
        public IHttpActionResult PosttblHashtagAds(tblHashtagAd tblHashtagAds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblHashtagAds(tblHashtagAds.HashtagID, tblHashtagAds.AdsID, tblHashtagAds.IsDeleted);
            
            return CreatedAtRoute("DefaultApi", new { id = tblHashtagAds.HashtagAdsID }, tblHashtagAds);
        }

        // DELETE: api/tblHashtagAds/5
        [ResponseType(typeof(tblHashtagAd))]
        public IHttpActionResult DeletetblHashtagAds(int id)
        {

            db.sp_DeltblHashtagAds(id);
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

        private bool tblHashtagAdsExists(int id)
        {
            return db.tblHashtagAds.Count(e => e.HashtagAdsID == id) > 0;
        }
    }
}