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
using Android.Webkit;

namespace betbrane.Resources.Activities
{
    [Activity(Label = "Web View Sample", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
    public class LogIn : Activity
    {
        public class SSOID
        {
            public static string ssoid = "2gV/LxbQ3DFVIYYahxdz9u+aPscpDpIAugeaLCTnQ88=";
        }
        string url = "https://identitysso.betfair.com/view/login";
        //string url = "google.com";
        WebView web_view;
        SSOID ss = new SSOID();
     //   StartActivity(typeof(ViewSportsEvents));
        public class HelloWebViewClient : WebViewClient
        {
            //public override bool ShouldOverrideUrlLoading(WebView view, string url)
            //{
            //    view.LoadUrl(url);
            //    return true;
            //}
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(ViewSportsEvents));
            SetContentView(Resource.Layout.LogIn);
            string url = "https://identitysso.betfair.com/view/login";
        //    web_view = FindViewById<WebView>(Resource.Id.webview);
            web_view.Settings.JavaScriptEnabled = true;
            web_view.LoadUrl(url);

            web_view.SetWebViewClient(new HelloWebViewClient());

            //var layout = new LinearLayout(this);
            //layout.Orientation = Orientation.Vertical;
            //var button = new Button(this);
            //button.Text = "Submit";
            //layout.AddView(button);
            //var url = "https://identitysso.betfair.com/view/login";
            //var uri = Android.Net.Uri.Parse("https://identitysso.betfair.com/view/login");
            //var intent = new Intent(Intent.ActionView, uri);
            //StartActivity(intent);
            //   intent.coo

            //    var cookieHeader = CookieManager.Instance.GetCookie(url);
            CookieManager CookieManager = CookieManager.Instance;
            var cookies = CookieManager.Instance.GetCookie(url);
            String[] temp = cookies.Split();
            foreach (String c in temp)
            {
                int idx = c.IndexOf("=");
                if (idx != -1)
                {
                    string cookieName = c.Substring(0, idx).Trim();
                    string cookieval = c.Substring(idx + 1).Trim();

                    if (cookieName == "ssoid")
                    {
                        cookieval = cookieval.TrimEnd(cookieval[cookieval.Length - 1]);
                        SSOID.ssoid = cookieval;

                  //      if (SSOID.ssoid != null) { StartActivity(typeof(SelectMeeting)); }
                        //    return ssoid;

                    }
                }
            }
            //  return null;
            //}
            //   }
            //    StartActivity(typeof(LogIn));
            //   string[] cookieArray = uri.Document.Cookie.Split();
            //if (webBrowser1.Document.Cookie.Count() > 0)
            //{
            //    foreach (string c in cookieArray)
            //    {

            //        int idx = c.IndexOf("=");
            //        if (idx != -1)
            //        {
            //            string cookieName = c.Substring(0, idx).Trim();
            //            string cookieval = c.Substring(idx + 1).Trim();
            //            if (cookieName == "ssoid")
            //            {
            //                ssoid = cookieval;

            //            }

            //        }

            //    }
            //}
            //if (ssoid != null)
            //{
            //    this.Close();
            //    Request req = new Request();
            //    req.ssoid = ssoid;
            //    Form1 f1 = new Form1();
            //    f1.init();
            //}


            //   }





        }
        //private class MyWebViewClient : WebViewClient
        //{

        //    public override void OnPageFinished(WebView view, string url)
        //    {
        //        base.OnPageFinished(view, url);
        //        LogIn mn = new LogIn();
        //        string ssoid = mn.getCookie();
        //        var cookieHeader = CookieManager.Instance.GetCookie(url);
        //    }


        //    }

        //public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
        //{
        //    if (keyCode == Keycode.Back && web_view.CanGoBack())
        //    {
        //    //  ssoid = getCookie();
        //  //      ssoid = CookieManager.Instance.GetCookie(url);
        //        web_view.GoBack();
        //        return true;
        //    }

        //    return base.OnKeyDown(keyCode, e);
        //}

        //    public string getCookie()
        //{

        //    CookieManager CookieManager = CookieManager.Instance;
        //    var cookies = CookieManager.Instance.GetCookie(url);
        //    String[] temp = cookies.Split();
        //    foreach (String c in temp)
        //    {
        //        int idx = c.IndexOf("=");
        //        if (idx != -1)
        //        {
        //            string cookieName = c.Substring(0, idx).Trim();
        //            string cookieval = c.Substring(idx + 1).Trim();
        //            if (cookieName == "ssoid")
        //            {
        //                ssoid = cookieval;
        //                return ssoid;

        //            }     
        //        }       
        //    }
        //    return null;
        //}
    }
}