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
        private DBModel db = new DBModel();
        // GET: api/Orders
        public IEnumerable<Order> Get()
        {
            FrontPad frontPad = new FrontPad("","");
            List<Order> list = frontPad.Parse();
            return list;
        }

        [HttpGet]
        [Route("api/Orders/Empty")]
        public IEnumerable<Order> Empty()
        {
            FrontPad frontPad = new FrontPad("", "4");
            List<Order> list = frontPad.Parse();
            return list;
        }

        [HttpGet]
        [Route("api/Orders/Active/{login}")]
        public IEnumerable<Order> Active(string login)
        {
            FrontPad frontPad = new FrontPad(login, "12");
            List<Order> list = frontPad.Parse();
            foreach (var elem in list) {
                if (!SmsExists(Convert.ToInt32(elem.id),elem.phone))
                {
                    string phone = elem.phone;
                    phone = phone.Substring(1);
                    string s = elem.id.Substring(elem.id.Length - 4);
                    int i = Convert.ToInt32(s);
                    int code = 9999 - i;
                    SmsService.sendSms("7" + phone, code.ToString());
                    Sm sms = new Sm();
                    sms.orderid = Convert.ToInt32(elem.id);
                    sms.phone = elem.phone;
                    db.Sms.Add(sms);
                    db.SaveChanges();
                }
            }
            return list;
        }

        [HttpGet]
        [Route("api/Orders/Closed/{login}")]
        public IEnumerable<Order> Closed(string login)
        {
            FrontPad frontPad = new FrontPad(login, "10");
            List<Order> list = frontPad.Parse();
            return list;
        }

        

        [HttpGet]
        [Route("api/Finish/{id}")]
        // GET: api/Orders/Close/555555
        public string Finish(string id)
        {
            Order.CloseOrder(id);
            return "value";
        }


        // POST: api/Orders
        public void Post([FromBody]string value)
        {
        }

        private bool SmsExists(int id, string phone)
        {
            return db.Sms.Count(e => e.orderid == id && e.phone == phone) > 0;
        }
    }
}
