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
    public class tblIpAddressesController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblIpAddresses
        public IQueryable<tblIpAddress> GettblIpAddress()
        {
            return db.tblIpAddresses;
        }

        // GET: api/tblIpAddresses/5
        [ResponseType(typeof(tblIpAddress))]
        public IHttpActionResult GettblIpAddress(int id)
        {
            var tblIpAddress = db.GetByIdIpAddress(id);
            if (tblIpAddress == null)
            {
                return NotFound();
            }

            return Ok(tblIpAddress);
        }

        // PUT: api/tblIpAddresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblIpAddress(int id, tblIpAddress tblIpAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblIpAddress.IpAddressID)
            {
                return BadRequest();
            }

            db.Entry(tblIpAddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblIpAddressExists(id))
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

        // POST: api/tblIpAddresses
        [ResponseType(typeof(tblIpAddress))]
        public IHttpActionResult PosttblIpAddress(tblIpAddress tblIpAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblIpAddress(tblIpAddress.BrowserName, tblIpAddress.BrowserVersion, DateTime.Now.Date, tblIpAddress.IpAddress, tblIpAddress.LocationID, tblIpAddress.WindowsName);
            
            return CreatedAtRoute("DefaultApi", new { id = tblIpAddress.IpAddressID }, tblIpAddress);
        }

        // DELETE: api/tblIpAddresses/5
        [ResponseType(typeof(tblIpAddress))]
        public IHttpActionResult DeletetblIpAddress(int id)
        {
            tblIpAddress tblIpAddress = db.tblIpAddresses.Find(id);
            if (tblIpAddress == null)
            {
                return NotFound();
            }

            db.tblIpAddresses.Remove(tblIpAddress);
            db.SaveChanges();

            return Ok(tblIpAddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblIpAddressExists(int id)
        {
            return db.tblIpAddresses.Count(e => e.IpAddressID == id) > 0;
        }
    }
}