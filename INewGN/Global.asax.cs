using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using INewGN.Models;
using INewGN.Controllers;

namespace INewGN
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        private static Timer UpdateNewsTimer;
        private static Timer UpdateSitremaps;
        private static Timer UpdateKeywordTracker;

        private const String SiteMapNodeName = "url";

        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            UpdateNewsTimer = new System.Timers.Timer(20000);
            // Hook up the Elapsed event for the timer. 
            Utility u = new Utility();
            UpdateNewsTimer.Elapsed += u.UpdateNews;
            UpdateNewsTimer.Enabled = true;



            // KeywordTracker();

            // Because Not Exixt Here

            //UpdateSitremaps = new System.Timers.Timer(800000);
            //// Hook up the Elapsed event for the timer. 
            //UpdateSitremaps.Elapsed += u.UpdateXml;
            //UpdateSitremaps.Enabled = true;

            //statics
            Application.Add("Online", 0);

        }

        public int KeywordTracker()
        {

            using (dbNIGNEntities db = new dbNIGNEntities())
            {
                var q = db.tblNews.ToList();

                SyncController s = new SyncController();
                foreach (var item in q)
                {
                    s.UpdateHashtagKey(db, item.Title, item.tblRss.CategoryID);
                }
            }
            return 0;

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Add("Login", "1");
            Application.Lock();
            Application["Online"] = (int)Application["Online"] + 1;
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Application.Lock();
            Application["Online"] = (int)Application["Online"] - 1;
            Application.UnLock();
        }
    }
}
