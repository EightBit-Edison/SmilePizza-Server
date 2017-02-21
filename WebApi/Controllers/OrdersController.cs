using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: api/Orders
        public IEnumerable<Order> Get()
        {
            FrontPad frontPad = new FrontPad("");
            List<Order> list = frontPad.Parse();
            return list;
        }

        // GET: api/Orders/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        public void Post([FromBody]string value)
        {
        }
    }
}
