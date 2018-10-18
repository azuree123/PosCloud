using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApp.Controllers.WebApi
{
    public class SyncController : ApiController
    {
        // GET: api/Sync
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sync/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sync
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sync/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sync/5
        public void Delete(int id)
        {
        }
    }
}
