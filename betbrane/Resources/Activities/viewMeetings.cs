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
using betbrane.Rest;
using static betbrane.Resources.Activities.LogIn;

namespace betbrane.Resources.Activities
{
    [Activity(Label = "ViewSportsEvents")]
    public class ViewSportsEvents : Activity
    {   //This class returns a list of race meetings
        ListVenuesResponse ListVenue = new ListVenuesResponse();
        public List<ListVenuesRequest> requestList = new List<ListVenuesRequest>();
        RESTService rs = new RESTService();
      //  SSOID ss = new SSOID();
        public List<string> marketCountries = new List<string>(new string[] {
                                 "GB",
                                    "IE",
                                        "FR",
                                            "ZA"
                                                    });
        public string countrySelected = "GB";
        public string eventTypes = "7";


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ViewSportsEvents);
            string username = Intent.GetStringExtra("UserName") ?? "Data not available";
            var displayUserNameText = FindViewById<TextView>(Resource.Id.editTextSportsViewUserName);
          //  displayUserNameText.Text = "You are logged in as Gerry";
            listMarketVenues();

            var listView = FindViewById<ListView>(Resource.Id.listViewSportsEvents);
            var venuesString = new List<String>();

            var venues = ListVenue.result;
            //for(int i =0; i <= ListVenue.result[])
            foreach (ReturnedVenues v in venues)
            {
                venuesString.Add(v.venue.ToString());
            }

            //   var data = new string[]; 
            listView.Adapter = new ArrayAdapter(this, Resource.Xml.listViewTemplate, venuesString);
            listView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs position)
            {
                string s = SSOID.ssoid;

                String selectedMeet = (String)(listView.GetItemAtPosition(position.Position));
                MarketCatalogueResponse mcr = ListMarketCatalogue(selectedMeet);
                string json = JsonConvert.SerializeObject(mcr);
                Intent intent = new Intent(this, typeof(ViewEvents));
                intent.PutExtra("selected", json);
                StartActivity(intent);
            };
        }

        public ListVenuesResponse listMarketVenues()
        {

            ListVenuesRequest request = new ListVenuesRequest();
            VenueParams @params = new VenueParams();
            VenueFilter fil = new VenueFilter();
            List<string> eventTypeIds = new List<string>();
            eventTypeIds.Add(eventTypes);
            fil.eventTypeIds = eventTypeIds;
            fil.marketCountries = marketCountries;
            @params.filter = fil;

            request.@params = @params;

            requestList.Add(request);
            string json = JsonConvert.SerializeObject(request);
            json = rs.Invoke(json);

            ListVenue = JsonConvert.DeserializeObject<ListVenuesResponse>(json);
            return ListVenue;


        }
        public MarketCatalogueResponse ListMarketCatalogue(string ven)
        {
          
            MarketCatalogueRequest request = new MarketCatalogueRequest();
            Params param = new Params();
            Models.Filter filter = new Models.Filter();
            List<string> eventTypeids = new List<string>();
            // Public venues As New List(Of String)
            List<string> marketcountries = new List<string>();
            List<string> venues = new List<string>();
            List<string> marketTypeCodes = new List<string>();
            List<string> marketProjection = new List<string>();
            venue v = new venue();
            
            eventTypeids.Add("7");
            marketProjection.Add("MARKET_START_TIME");
            marketProjection.Add("RUNNER_DESCRIPTION");
            marketProjection.Add("EVENT");
            marketTypeCodes.Add("WIN");
            param.marketProjection = marketProjection;
            
            List<String> vens = new List<String>();

            venues.Add(ven);
            filter.venues = venues;
            filter.eventTypeIds = eventTypeids;
            filter.marketTypeCodes = marketTypeCodes;

            param.filter = filter;
            request.@params = param;
          
            string markets = JsonConvert.SerializeObject(request);
            markets = rs.Invoke(markets);
            MarketCatalogueResponse response = new MarketCatalogueResponse();
            response = JsonConvert.DeserializeObject<MarketCatalogueResponse>(markets);
            return response;




        }
    }
}
//var items = new List<SportsEvent>
//{
//    new SportsEvent { EventType = "Soccer", Event = "Man Utd v Spurs", BeginTime = DateTime.Parse("2017-03-17"), Venue = "Old Trafford", competitors = new List< Competitors>()
//    { new Competitors {competitorName = "Man Utd",layPrice="2.36",backPrice = "2.34" },
//      new Competitors {competitorName = "Spurs",layPrice="3.75",backPrice = "3.65" },
//      new Competitors {competitorName = "Draw",layPrice="3.3",backPrice = "3.25" }
//    } },
//    new SportsEvent { EventType = "Soccer", Event = "New Utd v Liverpool", BeginTime = DateTime.Parse("2017-03-17"), Venue = "St James Park", competitors = new List< Competitors>()
//    { new Competitors {competitorName = "New Utd",layPrice="1.79",backPrice = "1.78" },
//      new Competitors {competitorName = "Liverpool",layPrice="6.2",backPrice = "6" },
//      new Competitors {competitorName = "Draw",layPrice="3.65",backPrice = "3.6" }
//    } },
//     new SportsEvent { EventType = "Soccer", Event = "West Ham Utd v Watford", BeginTime = DateTime.Parse("2017-03-17"), Venue = "Ann Boylen Ground", competitors = new List< Competitors>()
//    { new Competitors {competitorName = "West Ham Utd",layPrice="4.2",backPrice = "4.1" },
//      new Competitors {competitorName = "Watford",layPrice="2",backPrice = "1.99" },
//      new Competitors {competitorName = "Draw",layPrice="3.9",backPrice = "3.85" }
//    } },
//      new SportsEvent { EventType = "Soccer", Event = "Sligo Rovers v Finn Harps", BeginTime = DateTime.Parse("2017-03-17"), Venue = "The Showgrounnds", competitors = new List< Competitors>()
//    { new Competitors {competitorName = "Sligo Rovers",layPrice="1.55",backPrice = "1.54" },
//      new Competitors {competitorName = "Finn Harps",layPrice="7.2",backPrice = "7" },
//      new Competitors {competitorName = "Draw",layPrice="4.8",backPrice = "4.6" }
//    } },
//       new SportsEvent { EventType = "Soccer", Event = "Chelsea v Everton", BeginTime = DateTime.Parse("2017-03-17"), Venue = "Stamford Bridge", competitors = new List< Competitors>()
//    { new Competitors {competitorName = "Chelsea",layPrice="1.23",backPrice = "1.22" },
//      new Competitors {competitorName = " Everton",layPrice="22",backPrice = "20" },
//      new Competitors {competitorName = "Draw",layPrice="7.6",backPrice = "7.4" }
//    } },

//    new SportsEvent { EventType = "Horse Racing", Event = "The Gold Cup", BeginTime = DateTime.Parse("2017-03-17") },
//    new SportsEvent { EventType = "Horse Racing", Event = "The Pertemps Cup", BeginTime = DateTime.Parse("2017-03-17") },
//    new SportsEvent { EventType = "Greyhound Racing", Event = "The Derby", BeginTime = DateTime.Parse("2017-03-17") }
//};

//string eventType = Intent.GetStringExtra("selectedSport") ?? "Data not available";
//var editedList = new List<SportsEvent>();

//var displayUserNmae = FindViewById<EditText>(Resource.Id.editTextSportEventsUserName);
//displayUserNmae.Text = "You are logged in as Gerry";

//foreach (SportsEvent s in items) {
//    if (s.EventType == eventType)
//        editedList.Add(s);

//    editedList.Count();          
//}
//var data = new string[editedList.Count];
//SportsEvent[] array1 = new SportsEvent[editedList.Count];

//for (int i =0; i <= data.Count()-1; i++) {
//    data[i] = editedList[i].Event.ToString() + " : " + System.Environment.NewLine + editedList[i].BeginTime.ToString();
//}

//for (int j = 0; j <= array1.Count() - 1; j++)
//{
//    array1[j] = editedList[j];
//}

//var listVewSportsEvents = FindViewById<ListView>(Resource.Id.listViewSportsEvents);
//listVewSportsEvents.Adapter = new ArrayAdapter(this, Resource.Xml.listViewTemplate, data);



//listVewSportsEvents.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs position)
//{
//    string selectedEvent = (string)(listVewSportsEvents.GetItemAtPosition(position.Position));
//    SportsEvent selectedSportsEvent = new SportsEvent();
//    List<Competitors> eventCompetitors = new List<Competitors> ();

//    selectedSportsEvent.competitors = eventCompetitors;


//    int idx = selectedEvent.IndexOf(" :");

//    selectedEvent = selectedEvent.Substring(0, idx).Trim();


//    foreach (SportsEvent se in editedList) {
//        if (se.Event == selectedEvent)  selectedSportsEvent = se; }
//    Intent intent = new Intent(this, typeof(SubmitTrades));
//    intent.PutExtra("selectedEvent", JsonConvert.SerializeObject(selectedSportsEvent));

//    StartActivity(intent);

//};