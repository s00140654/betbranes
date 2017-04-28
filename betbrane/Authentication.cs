using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace betbrane
{
    class Authentication
    {
        public void Login()
        {

            string postData = "username=gerryg&password=1sideside";
            //  X509Certificate2 cert = new X509Certificate2(@"C:\client-2048.p12", "");
            //   X509Certificate2 cert = new X509Certificate2(@"Resources\layout\NewFolder1", "");
            X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            WebRequest request = WebRequest.Create("https://identitysso.betfair.com/api/certlogin");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("X-Application: sapYC9aErQI3tsQW");
            //  request.Credentials.Equals(cert);
            request.ContentType = "application/json";
            var stream = request.GetRequestStream();
            StreamWriter sw = new StreamWriter(stream, Encoding.Default);
            sw.Write(postData);
            string result;

            using (var resp = request.GetResponse())
            {
                result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                //  result = JObject.Parse(results);
            }
        }

    }
}