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
    public class tblRegionsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblRegions
        public IHttpActionResult GettblRegions()
        {
            return Json(db.tblRegions);
        }

        // GET: api/tblRegions/5
        [ResponseType(typeof(tblRegion))]
        public IHttpActionResult GettblRegions(int id)
        {

            
            var tblRegions = db.tblRegions.Where(p => p.RegionID == id).Select(p => new { p.CityID, p.RegionID, p.RegionName }).ToList();
            if (tblRegions == null)
            {
                return NotFound();
            }

            return Json(tblRegions);
        }

   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblRegionsExists(int id)
        {
            return db.tblRegions.Count(e => e.RegionID == id) > 0;
        }
    }
}