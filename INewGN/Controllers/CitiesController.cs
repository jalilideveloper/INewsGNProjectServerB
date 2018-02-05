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
    public class CitiesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblCities
        public IHttpActionResult GettblCity()
        {
            return Json(db.tblCities.Select(p => new { p.CityID, p.CityName, p.CountryID, p.ProvinceId }).ToList());
        }

        // GET: api/tblCities/5
        [ResponseType(typeof(tblCity))]
        public IHttpActionResult GettblCity(int id)
        {
            var tblCity = db.tblCities.Where(p => p.ProvinceId == id).Select(p=> new {p.CityID,p.CityName,p.CountryID,p.ProvinceId }).ToList() ;
            if (tblCity == null)
            {
                return NotFound();
            }

            return Json(tblCity);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCityExists(int id)
        {
            return db.tblCities.Count(e => e.CityID == id) > 0;
        }
    }
}