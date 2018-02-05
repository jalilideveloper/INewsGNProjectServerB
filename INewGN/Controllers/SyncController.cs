using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using INewGN.Models;
using System.IO;
using System.Xml;
using System.Text;


namespace INewGN.Controllers
{
    public class SyncController : Controller
    {
        // GET: Sync
        public void GetNewData_Compare_AndInsertToDb()
        {
            Utility.FinalTaskNumber = 1;
            try
            {
                using (dbNIGNEntities db = new dbNIGNEntities())
                {
                    IList<tblNew> TempList = new List<tblNew>();
                    Random rnd = new Random();
                    var qRss = db.GetAllRssCategory();
                    foreach (var item in qRss)
                    {
                        IList<Item> lst = ParseRss(item.RssID, item.RssUrl, Convert.ToInt32(item.RssTypeID));
                        var q = db.GetAllNewsForCompareWithNewNews(item.RssID).FirstOrDefault();
                        if (q != null)
                        {
                            foreach (var pItem in lst)
                            {
                                if (pItem.Title != q.Title)
                                {
                                    TempList.Add(new tblNew
                                    {
                                        Description = pItem.Content,
                                        ImageUrl = "",
                                        IsDeleted = false,
                                        LanguageID = item.LanguageID,
                                        LikeCount = 0,
                                        Likes = 0,
                                        RssID = item.RssID,
                                        PublishDate = pItem.PublishDate,
                                        RegisterDate = DateTime.Now.Date,
                                        Title = pItem.Title,
                                        Unlike = 0,
                                        ViewNumber = rnd.Next(10, 15),
                                        Link = pItem.Link
                                    });

                                }
                                else
                                {
                                    break;
                                }
                            }

                            var reverseList = TempList.Reverse().ToList();
                            foreach (var itemInside in reverseList)
                            {
                                using (var ctx = new dbNIGNEntities())
                                {
                                    try
                                    {
                                        if (item.RssTypeID == 5)
                                        {
                                             ctx.sp_Add_tblNews(itemInside.Title, itemInside.Description, itemInside.PublishDate, DateTime.Now.Date, itemInside.ImageUrl, rnd.Next(10, 15), itemInside.Link, item.LanguageID, item.RssID, 0, 0, 0, false,item.CategoryID);
                                        }
                                        else
                                        {
                                            ctx.sp_Add_tblNews(itemInside.Title, itemInside.Description, itemInside.PublishDate, DateTime.Now.Date, "", rnd.Next(10, 15), itemInside.Link, item.LanguageID, item.RssID, 0, 0, 0, false, item.CategoryID);
                                        }

                                      //  UpdateHashtagKey(db, itemInside.Title, item.CategoryID);

                                        XmlWriterSettings settings = new XmlWriterSettings();
                                        settings.Encoding = Encoding.UTF8;
                                        settings.Indent = true;

                                        //Utility.UpdateAllNewsSitemap(ctx, settings, new Utility.XmlSitemaps { loc = itemInside.LinkUrl, lastmod = itemInside.PubDate.ToString() });
                                        //Utility.UpdateArticleSitemap(settings, new Utility.XmlSitemaps { loc = itemInside.LinkUrl, lastmod = itemInside.PubDate.ToString() });
                                    }
                                    catch (Exception e)
                                    {
                                        continue;

                                    }



                                }
                            }
                            TempList.Clear();
                        }
                        else if (q == null)
                        {
                            var SortedList = lst.Reverse().ToList();
                            foreach (var itemInside in SortedList)
                            {
                                using (var ctx = new dbNIGNEntities())
                                {
                                    try
                                    {
                                        if (item.RssTypeID == 5)
                                        {
                                            ctx.sp_Add_tblNews(itemInside.Title, itemInside.Content, itemInside.PublishDate, DateTime.Now.Date, itemInside.ImageUrl, rnd.Next(10, 15), itemInside.Link, item.LanguageID, item.RssID, 0, 0, 0, false, item.CategoryID);
                                        }
                                        else
                                        {
                                            ctx.sp_Add_tblNews(itemInside.Title, itemInside.Content, itemInside.PublishDate, DateTime.Now.Date, "", rnd.Next(10, 15), itemInside.Link, item.LanguageID, item.RssID, 0, 0, 0, false, item.CategoryID);
                                        }

                                      //  UpdateHashtagKey(db, itemInside.Title, item.CategoryID);
                                        XmlWriterSettings settings = new XmlWriterSettings();
                                        settings.Encoding = Encoding.UTF8;
                                        settings.Indent = true;


                                        //Utility.UpdateAllNewsSitemap(ctx, settings, new Utility.XmlSitemaps { loc = itemInside.Link, lastmod = itemInside.PublishDate.ToString() });
                                        //Utility.UpdateArticleSitemap(settings, new Utility.XmlSitemaps { loc = itemInside.Link, lastmod = itemInside.PublishDate.ToString() });
                                    }
                                    catch (Exception e)
                                    {
                                        continue;

                                    }
                                    //db.SaveChanges();
                                }
                            }
                        }
                    }
                    //return true;
                }
            }
            catch (Exception e)
            {
                // string q = e.InnerException.ToString();


            }
            Utility.FinalTaskNumber = 0;
        }

        public void UpdateHashtagKey(dbNIGNEntities db, string title, int? categoryID)
        {
            try
            {
                string[] spliter = title.Split(' ');
                for (int i = 0; i < spliter.Length; i++)
                {
                    string word = spliter[i];
                    db.sp_UpdateOrAdd_tblHashTag(word,categoryID);
                }
            }
            catch
            {

            }
            

        }

       

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public virtual IList<Item> ParseRss(int rssid, string url, int rsstypeID)
        {
            try
            {

                //XmlTextReader reader = new XmlTextReader(url);
                //DataSet ds = new DataSet();
                //ds.ReadXml(reader);


                //XmlDocument rssXmlDoc = new XmlDocument();
                //rssXmlDoc.Load(url);
               
                    var document = XDocument.Load(url);



                    XDocument doc = XDocument.Load(url);
                    // RSS/Channel/item





                    if (rsstypeID == 1)
                    {
                        try
                        {
                            var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                                          select new Item
                                          {
                                              FeedType = FeedType.RSS,
                                              Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                              Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                              PublishDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                                              Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                                          };
                            return entries.ToList();
                        }
                        catch
                        {
                            return new List<Item>();
                        }
                    }
                    else if (rsstypeID == 2)
                    {
                        var entries1 = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item").ToList();
                        List<Item> lstTemp = new List<Item>();
                        foreach (var item in entries1)
                        {
                            try
                            {
                               
                                Item objItem = new Item();
                                objItem.FeedType = FeedType.RSS;
                                objItem.Content = item.Elements().First(i => i.Name.LocalName == "description").Value;
                                objItem.Link = item.Elements().First(i => i.Name.LocalName == "link").Value;
                                objItem.PublishDate = DateTime.Now.Date;
                                objItem.Title = item.Elements().First(i => i.Name.LocalName == "title").Value;
                                lstTemp.Add(objItem);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        return lstTemp;
                    }

                else if (rsstypeID == 3)
                {
                    var entries1 = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item").ToList();
                    List<Item> lstTemp = new List<Item>();
                    foreach (var item in entries1)
                    {
                        try
                        {
                            Item objItem = new Item();
                            objItem.FeedType = FeedType.RSS;
                            objItem.Content = "";
                            objItem.Link = item.Elements().First(i => i.Name.LocalName == "link").Value;
                            objItem.PublishDate = DateTime.Now.Date;
                            objItem.Title = item.Elements().First(i => i.Name.LocalName == "title").Value;
                            lstTemp.Add(objItem);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    return lstTemp;
                }
               
                else if (rsstypeID == 5)
                {
                    var entries1 = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item").ToList();
                    List<Item> lstTemp = new List<Item>();
                    foreach (var item in entries1)
                    {
                        try
                        {
                            Item objItem = new Item();
                            objItem.FeedType = FeedType.RSS;
                            objItem.Content = "";
                            objItem.ImageUrl = item.Elements().First(i => i.Name.LocalName == "enclosure").Attribute("url").Value;
                            objItem.Link = item.Elements().First(i => i.Name.LocalName == "link").Value;
                            objItem.PublishDate = DateTime.Now.Date;
                            objItem.Title = item.Elements().First(i => i.Name.LocalName == "title").Value;
                            lstTemp.Add(objItem);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    return lstTemp;
                }


                else
                    {
                        return new List<Item>();
                    }
             
            }
            catch (Exception ex)
            {
                //string s = ex.InnerException.ToString();
                return new List<Item>();
            }
        }

        public T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public class Item
        {
            public string Link { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime? PublishDate { get; set; }
            public FeedType FeedType { get; set; }

            public string ImageUrl { get; set; }
            public Item()
            {
                Link = "";
                Title = "";
                Content = "";
                PublishDate = null;
                FeedType = FeedType.RSS;
                ImageUrl = "";
            }
        }
        public enum FeedType
        {
            /// <summary>
            /// Really Simple Syndication format.
            /// </summary>
            RSS,
            /// <summary>
            /// RDF site summary format.
            /// </summary>
            RDF,
            /// <summary>
            /// Atom Syndication format.
            /// </summary>
            Atom
        }
    }
}