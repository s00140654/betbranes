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
using System.IO;

namespace betbrane.Rest
{
    class RESTService
    {
        public string Invoke(string json)
        {
            string ssoid = "eXWjgGuVXvLLmujOFOPZQLFJeJvRiuvJdVJhIFsdSKw=";
            //string ssoid = SSOID.ssoid;
            string url = "https://api.betfair.com/exchange/betting/json-rpc/v1";
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(json);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.Headers.Add("X-Application: sapYC9aErQI3tsQW");
            request.Headers.Add("X-Authentication: " + ssoid);
            //request.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate;
            request.ServicePoint.Expect100Continue = false;
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8");
            request.Timeout = 10000;
            string result;



            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (var resp = request.GetResponse())
            {
                result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                //  result = JObject.Parse(results);
            }

            return result;
        }

    }
}