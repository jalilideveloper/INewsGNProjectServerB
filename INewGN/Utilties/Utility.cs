using INewGN.Controllers;
using INewGN.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;

namespace INewGN
{
    public class Utility
    {
        public static byte FinalTaskNumber = 0;

        public void UpdateNews(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                if (FinalTaskNumber == 0)
                {
                    SyncController s = new SyncController();
                    s.GetNewData_Compare_AndInsertToDb();
                }
            }
        }

        public void UpdateXml(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                using (dbNIGNEntities db = new dbNIGNEntities())
                {


                    var virPath = HostingEnvironment.MapPath("/");

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = Encoding.UTF8;
                    settings.Indent = true;


                    // All News
                    UpdateAllNewsSitemap(db, settings);

                    // All Number Pages
                    UpdateArticleSitemap(settings);

                    // All Magaznie 
                    UpdateMagazine(db, settings);


                    // All MagaznieSelected 
                    UpdateMagazineSelected(db, settings);

                }
            }

        }

        private static void UpdateMagazineSelected(dbNIGNEntities db, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "MagazineSelectedSitemap.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "MagazineSelectedSitemap.xml", settings))
                {
                    var magazines = db.GetAllRssCategory().ToList();
                    writer.WriteStartDocument();

                    writer.WriteStartElement("urlset");

                    foreach (var item in magazines)
                    {
                        DTController dt = new DTController();
                        int countMagazineSelected = Convert.ToInt32(dt.GetAllPages(item.RssID));

                        for (int i = 0; i < countMagazineSelected; i++)
                        {
                            writer.WriteStartElement("url");
                            //--------------------------------------
                            writer.WriteStartElement("loc");
                            writer.WriteString("https://greenoptimizer.com/Home/MagazineSelected/" + item.RssID + "-"+ i);
                            writer.WriteEndElement();
                            //--------------------------------------
                            writer.WriteEndElement();
                        }


                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }


            }
            else
            {
                if (newItem != null)
                {
                    XDocument xDocument = XDocument.Load(virPath + "MagazineSelectedSitemap.xml");
                    XElement root = xDocument.Element("urlset");
                    IEnumerable<XElement> rows = root.Descendants("url");
                    var magazines = db.GetAllRssCategory().ToList();
                    foreach (var item in magazines)
                    {
                        DTController dt = new DTController();
                        int countMagazineSelected = Convert.ToInt32(dt.GetAllPages(item.RssID));

                        //for (int i = 0; i < countMagazineSelected; i++)
                        //{
                        //    writer.WriteStartElement("url");
                        //    //--------------------------------------
                        //    writer.WriteStartElement("loc");
                        //    writer.WriteString("https://greenoptimizer.com/Home/MagazineSelected/" + item.MagazineID);
                        //    writer.WriteEndElement();
                        //    //--------------------------------------
                        //    writer.WriteEndElement();
                        //}


                    }
                    XElement firstRow = rows.First();
                    firstRow.AddBeforeSelf(
                       new XElement("url",
                      new XElement("loc", newItem.loc),
                       new XElement("lastmod", newItem.lastmod)));
                    xDocument.Save(virPath + "MagazineSelectedSitemap.xml");
                }
            }
        }

        private static void UpdateMagazine(dbNIGNEntities db, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "MagazineSitemap.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "MagazineSitemap.xml", settings))
                {
                    var magazines = db.GetAllRssCategory().ToList();
                    writer.WriteStartDocument();

                    writer.WriteStartElement("urlset");

                    foreach (var item in magazines)
                    {
                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/Magazine/" + item.RssID);
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }

            }
        }

        public static void UpdateArticleSitemap(XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "Magazine.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "Magazine.xml", settings))
                {
                    writer.WriteStartDocument();


                    DTController dt = new DTController();
                    int countAllNews = Convert.ToInt32(dt.GetAllPages(0));

                    writer.WriteStartElement("urlset");
                    for (int i = 0; i < countAllNews; i++)
                    {

                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/ArticleShow/" + i);
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }
            }
            else
            {

                if (newItem != null)
                {

                    DTController dt = new DTController();
                    int countallpages = Convert.ToInt32(dt.GetAllPages(0));

                    XDocument xDocument = XDocument.Load(virPath + "Magazine.xml");
                    XElement root = xDocument.Element("urlset");
                    IEnumerable<XElement> rows = root.Descendants("url");

                    if (rows.ToList().Count != countallpages)
                    {
                        for (int i =  rows.ToList().Count; i <= countallpages; i++)
                        {
                            XElement firstRow = rows.Last();
                            firstRow.AddAfterSelf(
                               new XElement("url",
                                 new XElement("loc", "https://greenoptimizer.com/Home/ArticleShow/" + i)
                                 ));
                            xDocument.Save(virPath + "Magazine.xml");
                        }
                    }
                }



            }
        }

        public static void UpdateAllNewsSitemap(dbNIGNEntities db, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "News.xml") == false)
            {
                using (XmlWriter writer = XmlWriter.Create(virPath + "News.xml", settings))
                {
                    var q = db.GetAllNews(1);
                    writer.WriteStartDocument();


                    writer.WriteStartElement("urlset");
                    foreach (var item in q.ToList())
                    {
                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/ArticleDetails/" + item.NewsID + "|" + item.Title.Replace(" ", "-").Replace("+", "-").Replace("?", "-").Replace("*", "-").Replace(";", "-").Replace(",", "-").Replace(".", "-").Replace(":", "-").Replace("؛", "-").Replace("؟", "-").Replace("»", "-").Replace("«", "-").Replace("!", "-").ToString());
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteStartElement("lastmod");
                        writer.WriteString(item.PublishDate.ToString());
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }
            }
            else
            {
                if (newItem != null)
                {

                    XDocument xDocument = XDocument.Load(virPath + "News.xml");
                    XElement root = xDocument.Element("urlset");
                    IEnumerable<XElement> rows = root.Descendants("url");
                    XElement firstRow = rows.Last();
                    firstRow.AddAfterSelf(
                       new XElement("url",
                       new XElement("loc", newItem.loc),
                       new XElement("lastmod", newItem.lastmod)));
                    xDocument.Save(virPath + "News.xml");
                }
            }
        }

        public class XmlSitemaps
        {

            public string loc { get; set; }

            public string lastmod { get; set; }


        }
    }
}