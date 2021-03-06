﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Parser.Html;

namespace WebApi.Models
{
    public class FrontPad
    {
        string html;
        public FrontPad(string user,string filter)
        {

            Task<string> task = Connect(user,filter);
            task.Wait();
            html = task.Result;
        }

        private async Task<string> Connect(string user,string filter)
        {
            DateTime current = DateTime.Today;
            current = current.AddHours(1);
            DateTime next =current.AddSeconds(86399);
            string sURL = "https://app.frontpad.ru/blocks/content/order_loader_new.php";
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(new Uri(sURL));
            request2.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request2.Referer = "https://app.frontpad.ru/";
            request2.Headers.Add("Cookie", "_ym_uid=1488403662144298347; PHPSESSID=n8qk9p7rc9dvimquaslkma6lg6; _ym_isad=2");
            request2.Method = "POST";
            string postData = "datetime1="+ current.ToString(@"dd.MM.yyyy HH:mm:ss")+ "& datetime2=" + next.ToString(@"dd.MM.yyyy HH:mm:ss") + "&filter_waiter=" + user+"&filter_status=" + filter + "&&filter_point=0,0&filter_pay=0&step=100&num=0";
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

        public string Get()
        {
            return html;
        }

        public List<Order> Parse()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(html);
            var numbers = document.QuerySelectorAll("div").Where(x => x.ClassName == "order_datetime");
            var address = document.QuerySelectorAll("div").Where(x => x.ClassName == "address");
            var phones = document.QuerySelectorAll("span").Where(x => x.ClassName == "btnImg call");
            var prices = document.QuerySelectorAll("select").Where(x => x.Id.StartsWith("select_waiter"));
            var addressEnum = address.GetEnumerator();
            var phoneEnum = phones.GetEnumerator();
            var priceEnum = prices.GetEnumerator();

            List<Order> res = new List<Order>();

            foreach (var number in numbers)
            {
                Order curr = new Order();
                Regex pattern = new Regex(@"\((?<val>.*?)\)",
                RegexOptions.Compiled |
                RegexOptions.Singleline);
                Match m = pattern.Match(number.InnerHtml);
                if (m.Success) curr.number = m.Groups["val"].Value;

                addressEnum.MoveNext();
                if (addressEnum.Current != null)
                {
                    curr.address = addressEnum.Current.InnerHtml;
                }

                phoneEnum.MoveNext();
                    pattern = new Regex(@"\(\'(?<val>.*?)\'\)",
                        RegexOptions.Compiled |
                        RegexOptions.Singleline);
                    string ph = "";
                    if (phoneEnum.Current != null && phoneEnum.Current.GetAttribute("onclick") != null)
                    {
                        ph = phoneEnum.Current.GetAttribute("onclick");
                    }
                    m = pattern.Match(ph);
                
                if (m.Success) curr.phone = m.Groups["val"].Value;
                res.Add(curr);

                priceEnum.MoveNext();
                if (priceEnum.Current != null)
                {
                    curr.id = priceEnum.Current.Id.Substring(13);
                }

            }

            return res;


        }


    }
}