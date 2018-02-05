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
    public class ProvincesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/Provinces
        public IHttpActionResult GettblProvince()
        {
            return Json(db.tblProvinces.ToList().Select(p=> new {p.ProvinceID,p.ProvinceName }));
        }

        // GET: api/Provinces/5
        [ResponseType(typeof(tblProvince))]
        public IHttpActionResult GettblProvince(int id)
        {
            tblProvince tblProvince = db.tblProvinces.Find(id);
            if (tblProvince == null)
            {
                return NotFound();
            }

            return Json(tblProvince);
        }

     

     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProvinceExists(int id)
        {
            return db.tblProvinces.Count(e => e.ProvinceID == id) > 0;
        }
    }
}