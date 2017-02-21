using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebApi.Models
{
    public static class SmsService
    {
        public static async void sendSms(string text)
        {
            string sURL = "https://sms.ru/sms/send?api_id=6F97349E-1682-698C-6C7B-8149E6E97EA4&to=79103189985&text=Никита,+я+тестирую+курьера";
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(new Uri(sURL));
            request2.Method = "GET";
            Stream response =  request2.GetResponse().GetResponseStream();
            
        }
    }
}