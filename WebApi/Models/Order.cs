using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebApi.Models
{
    public class Order
    {
        public string id;
        public string number;
        public string address;
        public string phone;
        public static void CloseOrder(string order)
        {
            string sURL = "https://app.frontpad.ru/blocks/function/status.php";
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(new Uri(sURL));
            request2.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request2.Referer = "https://app.frontpad.ru/";
            request2.Headers.Add("Cookie", "_ym_uid=1488403662144298347; PHPSESSID=n8qk9p7rc9dvimquaslkma6lg6; _ym_isad=2");
            request2.Method = "POST";
            string postData = "status=10&orderID="+order;
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

        }
    }
}