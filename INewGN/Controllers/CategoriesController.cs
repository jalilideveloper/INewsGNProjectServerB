using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using INewGN.Models;
using System.Web.Script.Serialization;
using INewGN.Utilties;
using System.Collections;

namespace INewGN.Controllers
{
    public class CategoriesController : ApiController
    {
        // GET: api/Categories
        public string Get()
        {
            using (dbNIGNEntities db = new dbNIGNEntities())
            {
                if (DataCenter.lstcsategories.Count > 0)
                {
                    return new JavaScriptSerializer().Serialize(DataCenter.lstcsategories);
                }
                else
                {
                    var q = db.GetAllCategories(1).Select(n => new tblCategory { CategoryID = n.CategoryID, IsDeleted = n.IsDeleted, CategoryName = n.CategoryName, ParrentID = n.ParrentID, LanguageID = n.LanguageID }).ToList();
                    DataCenter.lstcsategories = q;
                    return new JavaScriptSerializer().Serialize(q);
                }
            }
        }

        // GET: api/Categories/0-Sports     => 0 equal langid and Sports equal catname
        // GET: api/Categories/0-Sports-1     => 0 equal langid and Sports equal catname and 1 equal which 50 record od page
        public string Get(string id)
        {
            string[] arr = id.Split('-');
            tblCategory C = DataCenter.ReturnCategryID(arr[1]);
            if (arr[2] == "$1")
            {
                return GetTopNews(C.CategoryID, Convert.ToInt32(C.LanguageID), true);
            }
         
            else
            {
                return GetTopNews(Convert.ToInt32(arr[1]), Convert.ToInt32(arr[0]), false, Convert.ToInt32(arr[2]));
            }
        }

       
        // POST: api/Categories
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Categories/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Categories/5
        public void Delete(int id)
        {
        }

        public string GetTopNews(int cid, int clang, bool IsTop, int? PageNumber = 1)
        {
            using (dbNIGNEntities db = new dbNIGNEntities())
            {
                if (IsTop)
                {
                    var q = new JavaScriptSerializer().Serialize(db.GetToNewsEachCategory(cid, clang));
                    return q;
                }
                else
                {
                    var q = new JavaScriptSerializer().Serialize(db.GetLatestNewsOrderByDate(PageNumber, clang, cid));
                    return q;
                }
            }

        }

    }
}
