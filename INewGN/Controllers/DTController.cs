using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INewGN.Models;
using Newtonsoft.Json;

namespace INewGN.Controllers
{
    public class DTController : Controller
    {

        public string GetMostRecentNews()
        {
            //db.Database.Connection.Open();
            using (dbNIGNEntities dbs = new dbNIGNEntities())
            {

                var GetMostRecent = dbs.GetAllRecentNewsTopView().Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, RssMagazineName = p.RssName, ViewNumber = p.ViewNumber });
                var json = JsonConvert.SerializeObject(GetMostRecent);
                return json;
            }


        }



        public string GetAllNews(int id)
        {
            using (dbNIGNEntities db = new dbNIGNEntities())
            {

                List<listNews> lstNews = new List<listNews>();

                var GetallNews = db.GetLatestNewsOrderByDate(id, 1, 1)
                        .Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, RssMagazineName = p.RssName, ViewNumber = p.ViewNumber, Description = p.Description, PubDate = p.PublishDate.ToString() }).ToList();
                if (GetallNews.Count > 0)
                {
                    foreach (var item in GetallNews)
                    {
                        System.Globalization.PersianCalendar persianCalandar =
                                                     new System.Globalization.PersianCalendar();
                        if (item.PubDate != null)
                        {
                            int year = persianCalandar.GetYear(Convert.ToDateTime(item.PubDate));
                            int month = persianCalandar.GetMonth(Convert.ToDateTime(item.PubDate));
                            int day = persianCalandar.GetDayOfMonth(Convert.ToDateTime(item.PubDate));
                            lstNews.Add(new listNews { SiteTitle = item.SiteTitle, Description = item.Description, RssMagazineName = item.RssMagazineName, NewsID = item.NewsID, NTitle = item.NTitle, PubDate = year.ToString() + "/" + month.ToString() + "/" + day.ToString(), ViewNumber = item.ViewNumber });
                        }
                        else
                        {
                            lstNews.Add(new listNews { SiteTitle = item.SiteTitle, Description = item.Description, RssMagazineName = item.RssMagazineName, NewsID = item.NewsID, NTitle = item.NTitle, PubDate = "", ViewNumber = item.ViewNumber });
                        }
                    }
                    var json = JsonConvert.SerializeObject(lstNews);
                    return json;

                }
                else
                {
                    return "";
                }


            }
        }
        public string GetAllPages(int id)
        {
            using (dbNIGNEntities db = new dbNIGNEntities())
            {
                //id == 0 means article page
                //id == !0 magazine id
                List<Numbers> lstNews = new List<Numbers>();

                //long  GetallNews = db.spgo_GetPageNumbers(1,1).FirstOrDefault();

                long GetallNews = 500;
                string sectionPage = Convert.ToString(GetallNews / 15);

                return sectionPage;


            }
        }

        public string GetAllNewsMagazine(int id, int start)
        {
            using (dbNIGNEntities db = new dbNIGNEntities())
            {


                List<listNews> lstNews = new List<listNews>();

                var GetallNews = db.GetLatestNewsOrderByDateByRssID(id, start, 1)
                    .Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, RssMagazineName = p.RssName, ViewNumber = p.ViewNumber, Description = p.Descriptions, PubDate = p.PubDate.ToString() }).ToList();
                if (GetallNews.Count > 0)
                {


                    foreach (var item in GetallNews)
                    {
                        System.Globalization.PersianCalendar persianCalandar =
                                                     new System.Globalization.PersianCalendar();
                        if (item.PubDate != null)
                        {
                            int year = persianCalandar.GetYear(Convert.ToDateTime(item.PubDate));
                            int month = persianCalandar.GetMonth(Convert.ToDateTime(item.PubDate));
                            int day = persianCalandar.GetDayOfMonth(Convert.ToDateTime(item.PubDate));
                            lstNews.Add(new listNews { SiteTitle = item.SiteTitle, Description = item.Description, RssMagazineName = item.RssMagazineName, NewsID = item.NewsID, NTitle = item.NTitle, PubDate = year.ToString() + "/" + month.ToString() + "/" + day.ToString(), ViewNumber = item.ViewNumber });
                        }
                        else
                        {
                            lstNews.Add(new listNews { SiteTitle = item.SiteTitle, Description = item.Description, RssMagazineName = item.RssMagazineName, NewsID = item.NewsID, NTitle = item.NTitle, PubDate = "", ViewNumber = item.ViewNumber });
                        }
                    }
                    var json = JsonConvert.SerializeObject(lstNews);
                    return json;

                }
                else
                {
                    return "";
                }


            }
        }
        public string GetMagazines()
        {
            using (dbNIGNEntities db = new dbNIGNEntities())
            {
                var getMagazine = db.GetAllRssCategory().Select(p => new RssObject { RssID = p.RssID, RssName = p.Title });
                var json = JsonConvert.SerializeObject(getMagazine);
                return json;
            }
        }



        public string GetDistinctMagazines()
        {
            using (dbNIGNEntities db = new dbNIGNEntities())
            {
                var getMagazine = db.GetAllRssCategory().Select(p => new RssObject { RssID = p.RssID, RssName = p.Title }).Distinct().ToList();
                var json = JsonConvert.SerializeObject(getMagazine);
                return json;
            }

        }


        public string GetLatestNewsByMagazineID(int id)

        {
            using (dbNIGNEntities dbs = new dbNIGNEntities())
            {

                var qGetNewOFMAgazine = dbs.GetLatestNewsOrderByDateByRssID(id, 0, 1).Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, RssMagazineName = p.RssName, ViewNumber = p.ViewNumber });
                var json = JsonConvert.SerializeObject(qGetNewOFMAgazine);
                return json;
            }
        }



        // GET: DT
        public ActionResult Index()
        {
            return View();
        }

        // GET: DT/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DT/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DT/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DT/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DT/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DT/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

    public class listNews
    {

        public int NewsID { get; set; }
        public string NTitle { get; set; }
        public string RssMagazineName { get; set; }
        public long? ViewNumber { get; set; }
        public string PubDate { get; set; }
        public string Description { get; set; }

        public string SiteTitle { get; set; }

    }


    public class RssObject
    {
        public int RssID { get; set; }
        public string RssName { get; set; }
    }
    public class Numbers
    {
        public int count { get; set; }
    }
}