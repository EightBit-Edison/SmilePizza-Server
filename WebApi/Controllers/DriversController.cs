using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;


namespace WebApi.Controllers
{
    public class DriversController : ApiController
    {
       

        // GET: api/Drivers
        public List<Driver> GetDriver()
        {
            Task<string> task = Connect();
            task.Wait();
            string html = task.Result;
            //return html;
            return Parse(html);
        }

        // GET: api/Drivers/22832
        [HttpGet]
        [Route("api/Driver/{login}")]
        
        public Driver GetDriver(string login)
        {
            Task<string> task = Connect();
            task.Wait();
            string html = task.Result;
            List<Driver> drivers = Parse(html);
            return drivers.FirstOrDefault(x => x.login == login);
        }


        

        private async Task<string> Connect()
        {
            DateTime current = DateTime.Today;
            current = current.AddHours(1);
            DateTime next = current.AddSeconds(86399);
            string sURL = "https://app.frontpad.ru/main/employee.php";
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(new Uri(sURL));
            request2.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request2.Referer = "https://app.frontpad.ru/";
            request2.Headers.Add("Cookie", "_ym_uid=1488403662144298347; PHPSESSID=n8qk9p7rc9dvimquaslkma6lg6; _ym_isad=2");
            request2.Method = "POST";
            string postData = "datetime1=" + current.ToString(@"dd.MM.yyyy HH:mm:ss") + "& datetime2=" + next.ToString(@"dd.MM.yyyy HH:mm:ss") + "&filter_waiter=12226&filter_status=12&&filter_point=0,0&filter_pay=0&step=100&num=0";
            //string postData = "datetime1=22.02.2017 01:00:00& datetime2=23.02.2017 00:59:59&filter_waiter=" + user + "&filter_status=" + filter + "&&filter_point=0,0&filter_pay=0&step=100&num=0";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request2.ContentType = "application/x-www-form-urlencoded";
            request2.ContentLength = byteArray.Length;
            Stream dataStream = request2.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            Stream response = new MemoryStream();
            response = request2.GetResponse().GetResponseStream();
            Encoding enc = Encoding.GetEncoding(1251);
            var objReader = new StreamReader(response, enc);
            string res = objReader.ReadToEnd();
            return res;
        }

        private List<Driver> Parse(string source)
        {
            List<Driver> drivers = new List<Driver>();
            var parser = new HtmlParser();
            var document = parser.Parse(source);
            var waiters = document.QuerySelectorAll("a");
            foreach (var waiter in waiters)
            {
                Driver driver = new Driver();
                driver.name = waiter.InnerHtml;
                string s = waiter.Attributes[1].Value;
                var pattern = new Regex(@"\(\'(?<val0>.*?)\'\,\'(?<val1>.*?)\'\,\'(?<val>.*?)\'\,\'(?<val2>.*?)\'\,\'(?<val3>.*?)\'\)",
                        RegexOptions.Compiled |
                        RegexOptions.Singleline);
                var m = pattern.Match(s);

                if (m.Success) { driver.login = m.Groups["val"].Value;
                    driver.password = m.Groups["val"].Value;
                    drivers.Add(driver);
                }
                

            }
            return drivers;
        }
    }
}