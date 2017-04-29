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
using betbrane.Models;
using Newtonsoft.Json;
using betbrane.Resources.Back_Office_Work;
using betbrane.Rest;
using Java.Lang;
using System.Threading;

namespace betbrane.Resources.Activities
{
    [Activity(Label = "SubmitTrades")]
    public class SubmitTrades : Activity
    {
        //Timer timer = new Timer();
        static string message;
        int increment = 0;
        //Stake is hard coded in case my account gets cleaned out due to a coding error, in paractice bank would be a stop
        //loss, but for the case of testing there will be ony one hard coded stake of €
        static double stake = 2;
        static double bank;
        int amtOfbets = Convert.ToInt32( Java.Lang.Math.Floor(bank/stake));
        


        MarketCatalogue mc = new MarketCatalogue();
        PricesProcessing pc = new PricesProcessing();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SubmitTrades);
            var tv = new TextView(this);
            tv.Text = "message";
            LinearLayout ll = new LinearLayout(this);
            ll.AddView(tv);
            SportsEvent sportsEvent = new SportsEvent();
//string selectedEvent = Intent.GetStringExtra("selectedEvent") ?? "Data not available";
            
            
            string json = (Intent.GetStringExtra("selectedEvent") ?? "Data not available");
            mc = JsonConvert.DeserializeObject<MarketCatalogue>(json);

            var submitBankButton = FindViewById<Button>(Resource.Id.submitBank);
            var cancelTradesButton = FindViewById<Button>(Resource.Id.cancelBank);
            var confirmBankButton = FindViewById<Button>(Resource.Id.ConfirmBank);
            var displayEventInfo = FindViewById<TextView>(Resource.Id.displayEventInfo);


            var textBankAmount = FindViewById<TextView>(Resource.Id.textBankAmount);
            //Trade trade = new Trade();
            //trade.Bank = textBankAmount.Text;
            //sportsEvent.Trade = trade;
            bank = double.Parse(textBankAmount.Text);

            submitBankButton.Click += (sender, e) =>
            {
                confirmBankButton.Visibility = ViewStates.Visible;
                cancelTradesButton.Visibility = ViewStates.Visible;
                submitBankButton.Visibility = ViewStates.Invisible;
            };

            confirmBankButton.Click += (IntentSender, e) => {
                callProcess();
                System.Threading.Thread t1 = new System.Threading.Thread(new ThreadStart(callProcess));
                t1.Start();
                //Intent intent = new Intent(this, typeof(ShowTrades));
                
                //intent.PutExtra("key", (message));

                //Intent intent = new Intent(this, typeof(ShowTrades));
                ////intent.PutExtra("key", JsonConvert.SerializeObject(sportsEvent));
                //StartActivity(typeof(ShowTrades));
            };

            cancelTradesButton.Click += (sender, e) => {
                
                Intent intent = new Intent(this, typeof(MainActivity));

                StartActivity(intent);
            };



        }

        private void callProcess()
        {
            var mess = FindViewById<TextView>(Resource.Id.messageT);
            string i = Convert.ToString(increment);
            message = "Attempt " + i + "To get Matched";
            mess.Text = (message);
            

            System.Threading.Thread.Sleep(10000);
            pc.ProcessMarketData(stake, buildMarketBookRequest(mc));
            increment++;
            callProcess();
            //ShowTrades st = new ShowTrades();

        }

        private MarketBookResponse buildMarketBookRequest(MarketCatalogue mc)
        {
            RESTService rs = new RESTService();
            MarketBookRequest mbr = new MarketBookRequest();
            MarketBookParams mbrParams = new MarketBookParams();
            PriceProjection pro = new PriceProjection();
            var ids = new List<string>();
            var priceData = new List<string>();
            ids.Add(mc.marketId);
            mbrParams.marketIds = ids;
            priceData.Add("EX_BEST_OFFERS");
            priceData.Add("EX_TRADED");
            mbrParams.orderProjection = "ALL";
            pro.priceData = priceData;
            mbrParams.priceProjection = pro;
            mbr.@params = mbrParams;
            MarketBookResponse resp = new MarketBookResponse();
            resp = JsonConvert.DeserializeObject<MarketBookResponse>(rs.Invoke(JsonConvert.SerializeObject(mbr)));

            mbrParams.priceProjection = pro;
            return resp;

            //throw new NotImplementedException();
        }


    }
}
//displayEventInfo.Text = String.Format("Event Details\nVenue : {0}\n \n{1} Back Price : {2} Lay Price {3}\n \n{4} Back Price : {5} Lay Price {6} \n\n{7} Back Price : {8} Lay Price {9}\n \nKO : {10} \n"
//                 ,sportsEvent.Venue 
//                 ,sportsEvent.competitors[0].competitorName, sportsEvent.competitors[0].backPrice,sportsEvent.competitors[0].layPrice
//                 ,sportsEvent.competitors[1].competitorName, sportsEvent.competitors[1].backPrice, sportsEvent.competitors[1].layPrice
//                 ,sportsEvent.competitors[2].competitorName, sportsEvent.competitors[2].backPrice, sportsEvent.competitors[2].layPrice
//                 ,sportsEvent.BeginTime);
