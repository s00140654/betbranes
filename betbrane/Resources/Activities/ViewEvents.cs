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
using Newtonsoft.Json;
using betbrane.Models;

namespace betbrane.Resources.Activities
{
    [Activity(Label = "ViewSports")]
    public class ViewEvents : Activity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewSports);
            #region
            SetContentView(Resource.Layout.ViewSportsEvents);
            var items = new List<SportsEvent>
            {
                new SportsEvent { EventType = "Soccer", Event = "Man Utd v Spurs", BeginTime = DateTime.Parse("2017-03-17"), Venue = "Old Trafford", competitors = new List< Competitors>()
                { new Competitors {competitorName = "Man Utd",layPrice="2.36",backPrice = "2.34" },
                  new Competitors {competitorName = "Spurs",layPrice="3.75",backPrice = "3.65" },
                  new Competitors {competitorName = "Draw",layPrice="3.3",backPrice = "3.25" }
                } },
                new SportsEvent { EventType = "Soccer", Event = "New Utd v Liverpool", BeginTime = DateTime.Parse("2017-03-17"), Venue = "St James Park", competitors = new List< Competitors>()
                { new Competitors {competitorName = "New Utd",layPrice="1.79",backPrice = "1.78" },
                  new Competitors {competitorName = "Liverpool",layPrice="6.2",backPrice = "6" },
                  new Competitors {competitorName = "Draw",layPrice="3.65",backPrice = "3.6" }
                } },
                 new SportsEvent { EventType = "Soccer", Event = "West Ham Utd v Watford", BeginTime = DateTime.Parse("2017-03-17"), Venue = "Ann Boylen Ground", competitors = new List< Competitors>()
                { new Competitors {competitorName = "West Ham Utd",layPrice="4.2",backPrice = "4.1" },
                  new Competitors {competitorName = "Watford",layPrice="2",backPrice = "1.99" },
                  new Competitors {competitorName = "Draw",layPrice="3.9",backPrice = "3.85" }
                } },
                  new SportsEvent { EventType = "Soccer", Event = "Sligo Rovers v Finn Harps", BeginTime = DateTime.Parse("2017-03-17"), Venue = "The Showgrounnds", competitors = new List< Competitors>()
                { new Competitors {competitorName = "Sligo Rovers",layPrice="1.55",backPrice = "1.54" },
                  new Competitors {competitorName = "Finn Harps",layPrice="7.2",backPrice = "7" },
                  new Competitors {competitorName = "Draw",layPrice="4.8",backPrice = "4.6" }
                } },
                   new SportsEvent { EventType = "Soccer", Event = "Chelsea v Everton", BeginTime = DateTime.Parse("2017-03-17"), Venue = "Stamford Bridge", competitors = new List< Competitors>()
                { new Competitors {competitorName = "Chelsea",layPrice="1.23",backPrice = "1.22" },
                  new Competitors {competitorName = " Everton",layPrice="22",backPrice = "20" },
                  new Competitors {competitorName = "Draw",layPrice="7.6",backPrice = "7.4" }
                } },

                new SportsEvent { EventType = "Horse Racing", Event = "The Gold Cup", BeginTime = DateTime.Parse("2017-03-17") },
                new SportsEvent { EventType = "Horse Racing", Event = "The Pertemps Cup", BeginTime = DateTime.Parse("2017-03-17") },
                new SportsEvent { EventType = "Greyhound Racing", Event = "The Derby", BeginTime = DateTime.Parse("2017-03-17") }
            };
            #endregion
            var marketList = new List<string>();
            //  sportsEvent = JsonConvert.DeserializeObject<SportsEvent>(Intent.GetStringExtra("selectedEvent") ?? "Data not available");
            string json = Intent.GetStringExtra("selected") ?? "Data not available";
            MarketCatalogueResponse mcr = new MarketCatalogueResponse();
             mcr = JsonConvert.DeserializeObject<MarketCatalogueResponse>(json);
            foreach (MarketCatalogue e in mcr.result)
            {
                string s = e.marketId + " : " + System.Environment.NewLine + e.marketStartTime + " " + e.marketName;
                marketList.Add(s);
            }

            var listVewSportsEvents = FindViewById<ListView>(Resource.Id.listViewSportsEvents);
            listVewSportsEvents.Adapter = new ArrayAdapter(this, Resource.Xml.listViewTemplate, marketList);



            listVewSportsEvents.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs position)
            {
                string selectedEvent = (string)(listVewSportsEvents.GetItemAtPosition(position.Position));
                MarketCatalogue selected = new MarketCatalogue();
                List<Competitors> eventCompetitors = new List<Competitors>();

                int idx = selectedEvent.IndexOf(" : ");

                selectedEvent = selectedEvent.Substring(0, idx).Trim();
                //data[i] = products[i].productName + " : " + System.Environment.NewLine + "€"+ products[i].productPrice + " + ";
                //int idx = selected.IndexOf(" :");
                //selected = selected.Substring(0, idx).Trim();
                foreach (MarketCatalogue mc in mcr.result)
                {
                    if (mc.marketId.ToString() == selectedEvent) selected = mc;
                }

                Intent intent = new Intent(this, typeof(SubmitTrades));
                intent.PutExtra("selectedEvent", JsonConvert.SerializeObject(selected));

                StartActivity(intent);

            };

        }
    }
}

// var displayUserName = FindViewById<EditText>(Resource.Id.editTextSportEventsUserName);
// displayUserName.Text = "You are logged in as Gerry";
//for (int j = 0; j <= array1.Count() - 1; j++)
//            {
//                array1[j] = editedList[j];
//            }


//string username = Intent.GetStringExtra("UserName") ?? "Data not available";
//var displayUserNameText = FindViewById<TextView>(Resource.Id.editTextSportsViewUserName);
//displayUserNameText.Text = "You are logged in as Gerry"; 


//var listView = FindViewById<ListView>(Resource.Id.listView1);
//var data = new string[] {
//"Soccer", "Horse Racing", "Greyhound Racing", "Tennis", "Golf", "American Football", "Athletics"
//};
//listView.Adapter = new ArrayAdapter(this, Resource.Xml.listViewTemplate, data);
//listView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs position)
//{
//    String selectedSport = (String)(listView.GetItemAtPosition(position.Position));
//    Intent intent = new Intent(this, typeof(ViewSportsEvents));
//              intent.PutExtra("selectedSport", selectedSport);
//              StartActivity(intent);

//};
