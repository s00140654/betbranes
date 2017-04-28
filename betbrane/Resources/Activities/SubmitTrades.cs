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

namespace betbrane.Resources.Activities
{
    [Activity(Label = "SubmitTrades")]
    public class SubmitTrades : Activity
    {
        //Timer timer = new Timer();

        PricesProcessing pc = new PricesProcessing();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SubmitTrades);

            SportsEvent sportsEvent = new SportsEvent();
//string selectedEvent = Intent.GetStringExtra("selectedEvent") ?? "Data not available";
            MarketCatalogue mc = new MarketCatalogue();
            var displayUserName = FindViewById<TextView>(Resource.Id.displaySubmitTradesUserName);
            displayUserName.Text = "You are logged in as Gerry";
            string json = (Intent.GetStringExtra("selectedEvent") ?? "Data not available");
            mc = JsonConvert.DeserializeObject<MarketCatalogue>(json);

            var submitBankButton = FindViewById<Button>(Resource.Id.submitBank);
            var cancelTradesButton = FindViewById<Button>(Resource.Id.cancelBank);
            var confirmBankButton = FindViewById<Button>(Resource.Id.ConfirmBank);
            var displayEventInfo = FindViewById<TextView>(Resource.Id.displayEventInfo);


            var textBankAmount = FindViewById<TextView>(Resource.Id.textBankAmount);
            Trade trade = new Trade();
            trade.Bank = textBankAmount.Text;
            sportsEvent.Trade = trade;
            double stake = double.Parse(textBankAmount.Text);

            submitBankButton.Click += (sender, e) =>
            {
                confirmBankButton.Visibility = ViewStates.Visible;
                cancelTradesButton.Visibility = ViewStates.Visible;
                submitBankButton.Visibility = ViewStates.Invisible;
            };

            confirmBankButton.Click += (IntentSender, e) => {

                pc.ProcessMarketData(stake, buildMarketBookRequest(mc));
                Intent intent = new Intent(this, typeof(ShowTrades));
                intent.PutExtra("key", JsonConvert.SerializeObject(sportsEvent));
                StartActivity(intent);
            };

            cancelTradesButton.Click += (sender, e) => {
                Intent intent = new Intent(this, typeof(MainActivity));

                StartActivity(intent);
            };



        }

        private MarketBookResponse buildMarketBookRequest(MarketCatalogue mc)
        {
            RESTService rs = new RESTService();
            MarketBookRequest mbr = new MarketBookRequest();
            MarketBookParams mbrParams = new MarketBookParams();
            PriceProjection pro = new PriceProjection();
            var ids = new List<string>();
            var priceData = new List<String>();
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
