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
    public class tblUploadAdsFilesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblUploadAdsFiles
        public IQueryable<tblUploadAdsFile> GettblUploadAdsFile()
        {
            return db.tblUploadAdsFiles;
        }

        // GET: api/tblUploadAdsFiles/5
        [ResponseType(typeof(tblUploadAdsFile))]
        public IHttpActionResult GettblUploadAdsFile(int id)
        {
            var tblUploadAdsFile = db.GetByIdUploadAdsFile(id);
            if (tblUploadAdsFile == null)
            {
                return NotFound();
            }

            return Ok(tblUploadAdsFile);
        }

        // PUT: api/tblUploadAdsFiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUploadAdsFile(int id, tblUploadAdsFile tblUploadAdsFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUploadAdsFile.UploadAdsFileID)
            {
                return BadRequest();
            }

            db.Entry(tblUploadAdsFile).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUploadAdsFileExists(id))
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

        // POST: api/tblUploadAdsFiles
        [ResponseType(typeof(tblUploadAdsFile))]
        public IHttpActionResult PosttblUploadAdsFile(tblUploadAdsFile tblUploadAdsFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblUploadAdsFile(tblUploadAdsFile.AdsID, tblUploadAdsFile.FileAddress, false);
       

            return CreatedAtRoute("DefaultApi", new { id = tblUploadAdsFile.UploadAdsFileID }, tblUploadAdsFile);
        }

        // DELETE: api/tblUploadAdsFiles/5
        [ResponseType(typeof(tblUploadAdsFile))]
        public IHttpActionResult DeletetblUploadAdsFile(int id)
        {
            db.sp_DeltblUploadAdsFile(id);
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

        private bool tblUploadAdsFileExists(int id)
        {
            return db.tblUploadAdsFiles.Count(e => e.UploadAdsFileID == id) > 0;
        }
    }
}