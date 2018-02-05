using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using INewGN.Models;
using INewGN.Utilties;
using System.Data.SqlClient;

namespace INewGN.Controllers
{
    public class PageinationController : ApiController
    {
        // GET: api/Pageination
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pageination/1-Sport return count of all records in category
        public string Get(string id)
        {
            string[] arr = id.Split('-');
            if (arr[0] == "Tag")
            {
                var TagName = new SqlParameter("@TagName", System.Data.SqlDbType.NVarChar);
                var sqlParams = new[]
                {
                TagName
                };
                using (dbNIGNEntities db = new dbNIGNEntities())
                {
                    TagName.Value = arr[1];
                    return (db.Database.SqlQuery<int>("exec GetPageCountWithTagName @TagName,0,0", sqlParams).FirstOrDefault()).ToString();
                }
            }
            else if (arr[0] == "TagInCatgeory")
            {
                var TagName = new SqlParameter("@TagName", System.Data.SqlDbType.NVarChar);
                var CID = new SqlParameter("@CategoryID", System.Data.SqlDbType.Int);
                var sqlParams = new[]
                {
                TagName,CID
                };
                using (dbNIGNEntities db = new dbNIGNEntities())
                {
                    TagName.Value = arr[2];
                    CID.Value = arr[1];
                    return (db.Database.SqlQuery<int>("exec GetPageCountWithTagName @TagName,@CategoryID,0", sqlParams).FirstOrDefault()).ToString();
                }
            }
            else
            {
                var n1 = new SqlParameter("@CID", System.Data.SqlDbType.Int);
                var u1 = new SqlParameter("@langID", System.Data.SqlDbType.Int);
                var sqlParams = new[]
                {
                n1, u1
            };
                using (dbNIGNEntities db = new dbNIGNEntities())
                {
                    int CID = DataCenter.ReturnCategryID(arr[1]).CategoryID;
                    n1.Value = CID;
                    u1.Value = 1;
                    return (db.Database.SqlQuery<int>("exec spgo_GetPageNumbers @CID,@langID", sqlParams).FirstOrDefault()).ToString();
                }
            }
           
        }

        // POST: api/Pageination
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pageination/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pageination/5
        public void Delete(int id)
        {
        }
    }
}
