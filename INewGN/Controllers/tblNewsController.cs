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
    public class tblNewsController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();

        // GET: api/tblNews
        public IHttpActionResult GettblNews()
        {
            return Json(db.tblNews.Select(p=> new { Description = p.Description , ImageUrl  =p.ImageUrl, IsDeleted = p.IsDeleted, LanguageID  = p.LanguageID, LikeCount = p.LikeCount, Likes = p.Likes, Link = p.Link, NewsID = p.NewsID, PublishDate = p.PublishDate, RegisterDate = p.RegisterDate, RssID = p.RssID, Title = p.Title, Unlike = p.Unlike, ViewNumber = p.ViewNumber}).ToList());
        }

        // GET: api/tblNews/5
        [ResponseType(typeof(tblNew))]
        public IHttpActionResult GettblNews(string id)
        {
            string[] arr = id.Split('-');
            if (arr[0] == "All")
            {
                var tblNews = db.GetAllTop10News(Convert.ToInt32(arr[1]));
                if (tblNews == null)
                {
                    return NotFound();
                }
                return Ok(tblNews);
            }
            else if(arr[0] == "SearchedKeyGetCount")
            {
                var tblNews = db.GetCountSearchedKeyInDayMonthYear_Ago(arr[1],arr[2],0,false);
                if (tblNews == null)
                {
                    return NotFound();
                }
                return Ok(tblNews);

            }
            else if(arr[0] == "SearchedKeyGetCountCategory")
            {
                var tblNews = db.GetCountSearchedKeyInDayMonthYear_Ago(arr[1], arr[3],Convert.ToInt32(arr[2]),true);
                if (tblNews == null)
                {
                    return NotFound();
                }
                return Ok(tblNews);
            }
            else
            {

                var tblNews = db.GetByIdNews(Convert.ToInt32(id));
                if (tblNews == null)
                {
                    return NotFound();
                }

                return Ok(tblNews);
            }   
        }

        // PUT: api/tblNews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblNews(int id, tblNew tblNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblNews.NewsID)
            {
                return BadRequest();
            }

            db.Entry(tblNews).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblNewsExists(id))
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

        // POST: api/tblNews
        [ResponseType(typeof(tblNew))]
        public IHttpActionResult PosttblNews(tblNew tblNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sp_Add_tblNews(tblNews.Title, tblNews.Description, tblNews.PublishDate, DateTime.Now.Date, tblNews.ImageUrl, tblNews.ViewNumber, tblNews.Link, tblNews.LanguageID, tblNews.RssID, tblNews.LikeCount, tblNews.Unlike, tblNews.Likes, false,0);
            

            return CreatedAtRoute("DefaultApi", new { id = tblNews.NewsID }, tblNews);
        }

        // DELETE: api/tblNews/5
        [ResponseType(typeof(tblNew))]
        public IHttpActionResult DeletetblNews(int id)
        {

            db.sp_DeltblNews(id);
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

        private bool tblNewsExists(int id)
        {
            return db.tblNews.Count(e => e.NewsID == id) > 0;
        }
    }
}