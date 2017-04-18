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
        public static async void sendSms(string phone,string text)
        {
			text = "pizzeria: Ура! Ваш заказ едет к вам. подтвердите получение - скажите курьеру ключ " + text;
			string smpp = "<?xml version='1.0' encoding='utf - 8' ?>";
			smpp += "< request >";
			smpp += "< message type = 'sms' >";
			smpp += "< sender > SmilePizza </ sender >";
			smpp += "< text >"+text+"</ text >";
			smpp += "< abonent phone = '"+phone+"' number_sms = '1' />";
			smpp += "</ message >";
			smpp += "< security >";
			smpp += "< login value = 'user571' />";
			smpp += "< password value = 'user571' />";
			smpp += "</ security >";
			smpp += "</ request > ";
			string sURL = "http://lk.evatelecom.ru/xml/";
			HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(new Uri(sURL));
            request2.Method = "POST";
			string postData = smpp;
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
			            
        }
    }
}