using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class SmsController : ApiController
    {

        // GET: api/Sms/5
        public void Get(int id)
        {
            SmsService.sendSms("");
        }

        // POST: api/Sms
        public void Post([FromBody]string value)
        {
        }
    }
}
