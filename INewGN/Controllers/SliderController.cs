using INewGN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace INewGN.Controllers
{
    public class SliderController : ApiController
    {
        private dbNIGNEntities db = new dbNIGNEntities();
        // GET: api/Slider
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        //[ResponseType(typeof(tblNews))]
        // GET: api/Slider/5
        public IHttpActionResult Get(string id)
        {
            string[] arr = id.Split('-');
            if (arr !=  null && arr.Length > 0)
            {
                var NewsSlider = db.GetTopNewsSlider(Convert.ToInt32(arr[1]),Convert.ToInt32(arr[0]));
                if (NewsSlider == null)
                {
                    return NotFound();
                }
                return Ok(NewsSlider);
            }
            else
            {
                return NotFound();
            }
            
        }

        // GET: api/Slider/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Slider
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Slider/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Slider/5
        public void Delete(int id)
        {
        }
    }
}
