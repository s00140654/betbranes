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
using System.Collections.ObjectModel;
using System.Threading;

namespace betbrane.Resources.Activities
{
    [Activity(Label = "Show Trades")]
    public class ShowTrades : Activity
    {
        string[] data;
        public enum TradeType { Lay, Back };
        public enum Status { Open, Closed };
        public class TradeDTO
        {
            public string competitorName { get; set; }
            public TradeType TradeType { get; set; }
            public Status Status { get; set; }
            public double profit { get; set; }
            public double stake { get; set; }
            public double Price { get; set; }

            public TradeDTO(string cn, TradeType tt, Status ss, double pt, double se, double pe)
            {
                competitorName = cn;
                TradeType = tt;
                Status = ss;
                profit = pt;
                stake = se;
                Price = pe;

            }

        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ShowTrades);
            SportsEvent sportsEvent = new SportsEvent();
            List<TradeDTO> tradeDTOList = new List<TradeDTO>();

            var displayUserName = FindViewById<EditText>(Resource.Id.ShowTradesViewUserName);

            string message = (Intent.GetStringExtra("key") ?? "Data not available");
            displayUserName.Text = message;

            //    //sportsEvent = JsonConvert.DeserializeObject<SportsEvent>(Intent.GetStringExtra("key") ?? "Data not available");
            //    double pe = double.Parse(sportsEvent.competitors[0].backPrice);
            //    double se = double.Parse(sportsEvent.Trade.Bank);
            //    TradeDTO trade1 = new TradeDTO(sportsEvent.competitors[0].competitorName, TradeType.Lay, Status.Open, 0, se, pe );

            //    double npe = 0;
            //    Random random = new Random();
            //    double value = Math.Round((random.NextDouble() * (1.5 - .8) + .8), 2);
            //    npe  = pe * value;

            //    double nse = Math.Round(((se * pe)+se) / ((npe) + 1), 2);

            //    TradeDTO trade2 = new TradeDTO(sportsEvent.competitors[0].competitorName, TradeType.Lay, Status.Open, 0, nse, Math.Round(npe, 2));

            //    trade2.profit = Math.Round(((nse * npe)-(se * pe)), 2);
            //    trade1.profit = Math.Round((se - nse), 2);
            //    trade1.Status = Status.Closed;
            //    trade2.Status = Status.Closed;
            //    trade2.TradeType = TradeType.Back;
            //    tradeDTOList.Add(trade1);
            //    tradeDTOList.Add(trade2);
            //    data = new string[tradeDTOList.Count];
            //    //ObservableCollection<string> data = new ObservableCollection<string>();


            //    var listVewViewTrades = FindViewById<ListView>(Resource.Id.listViwShowTrades);
            //    for (int i = 0; i <= data.Count() - 1; i++)
            //    {
            //        data[i] = tradeDTOList[i].competitorName.ToString() + " - " + "Trade Type: "+ tradeDTOList[i].TradeType.ToString() + System.Environment.NewLine + 
            //                  "Price: " + tradeDTOList[i].Price.ToString() + " - " + "Stake: " + tradeDTOList[i].stake.ToString() +System.Environment.NewLine +
            //                  "Status: " + tradeDTOList[i].Status.ToString() +" - "+ "Profit: " + tradeDTOList[i].profit.ToString() ;
            //    }
            //    Thread t1 = new Thread(new ThreadStart(Sleep));
            //    t1.Start();
            //    listVewViewTrades.Adapter = new ArrayAdapter(this, Resource.Xml.listViewTemplate, data);

            //    var cashOutView = FindViewById<TextView>(Resource.Id.textDisplayProfit);
            //    var cashOutButton = FindViewById<Button>(Resource.Id.cashOutButton);
            //    cashOutButton.Text = "Cash Out for" + " " + trade1.profit.ToString();

            //    cashOutButton.Click += (sender, e) =>
            //    {
            //        Intent intent = new Intent(this, typeof(CashOut));
            //        intent.PutExtra("key", trade1.profit.ToString());
            //        StartActivity(intent);

            //    };

            //}

            //private void Sleep()
            //{
            //    Thread.Sleep(1000);

            //    // var listVewViewTrades = FindViewById<ListView>(Resource.Id.listViwShowTrades);
            //    //   listVewViewTrades.Adapter = new ArrayAdapter(this, Resource.Xml.listViewTemplate, data);

            
        }
    }
}