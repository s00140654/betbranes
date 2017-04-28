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
using Java.Lang;
using static Android.Content.ClipData;

namespace betbrane.Models
{
    public class SportsEvent
    {
        public string EventType { get; set; }

      public string Event { get; set; }

        public DateTime BeginTime { get; set; }

        public string Venue { get; set; }

        public List<Competitors> competitors;

        public Trade Trade { get; set; }

    }

    public class Competitors {
        public string competitorName { get; set; }

        public string backPrice { get; set; }

        public string layPrice { get; set; }

    }

    public class Trade{

        public string competitorName { get; set; }

        public enum Status {Open, Closed };

        public enum TradeType {Lay, Back };

        List <Competitors> Competitors { get; set; }

        public string Bank { get; set; }

        public double profit { get; set; }
    }

    public class ListVenuesRequest
    {
        public string jsonrpc = "2.0";
        public string method = "SportsAPING/v1.0/listVenues";
        public VenueParams @params = new VenueParams();
        public int id = 1;
    }



    public class VenueFilter
    {
        public List<string> eventTypeIds = new List<string>();
        public List<string> marketCountries = new List<string>();
        public StartTime marketStartTime = new StartTime();
    }
    public class VenueParams
    {
        public VenueFilter filter;
    }


    public class ListVenuesResponse
    {
        public string jsonrpc = "2.0";
        public List<ReturnedVenues> result { get; set; }
        public int id = 1;
    }

    public class ReturnedVenues
    {
        public string venue;
    }
    //Public Class StartTime
    //    Public from As String
    //    Public [to] As String
    //End Class


    public class MarketCatalogueRequest
    {
        public string jsonrpc = "2.0";
        public string method = "SportsAPING/v1.0/listMarketCatalogue";
        public Params @params = new Params();
        public int id = 1;
    }
    public class Params
    {

        public Filter filter { get; set; }
        public string sort = "FIRST_TO_START";
        public string maxResults = "20";
        public List<string> marketProjection { get; set; }
    }
    public class Filter
    {
        public List<string> eventTypeIds { get; set; }
        public List<string> marketCountries { get; set; }
        public List<string> marketTypeCodes { get; set; }
        public List<string> marketIds { get; set; }
        public List<string> venues { get; set; }

        public StartTime marketStartTime { get; set; }

    }
    


    public class StartTime
    {
        public string @from;
        public string to;
    }
    public class MarketCatalogueResponse
    {
        public string jsonrpc;
        public List<MarketCatalogue> result;
        public int id;
    }

    public class MarketCatalogue
    {
        public string marketId;
        public string marketName;
        public string marketStartTime;
        public double totalMatched;
        public List<Runners> runners { get; set; }

        public venue @event;
    }

    public class Runners
    {
        public int selectionId;
        public string runnerName;
        public double handicap;

        public int sortPriority;
    }


    public class venue
    {
        public int id;
        public string name;
        public string countryCode;
        public string timeZone;
        public string Venue;
        public string openDate;

    }

    public class MarketBookRequest
    {
        public string jsonrpc = "2.0";
        public string method = "SportsAPING/v1.0/listMarketBook";
        public MarketBookParams @params { get; set; }
        public int id = 1;
    }

    public class MarketBookParams
    {
        public List<string> marketIds { get; set; }
        public PriceProjection priceProjection { get; set; }
        public string orderProjection;
    }

    public class PriceProjection
    {
        public List<string> priceData { get; set; }
    }



    //Classes and functions for listMarketBook response
    public class MarketBookResponse
    {
        public string jsonrpc;
        public List<MarketBook> result { get; set; }
        public int id;
    }

    public class MarketBook
    {
        public string marketId;
        public bool isMarketDataDelayed;
        public string status;
        public int betDelay;
        public bool bspReconciled;
        public bool complete;
        public bool inplay;
        public int numberOfRunners;
        public int numberOfWinners;
        public int numberOfActiveRunners;
        public System.DateTime lastMatchTime;
        public double totalMatched;
        public double totalAvaialable;
        public bool crossMatching;
        public bool runnerVoidable;
        public int version;
        public List<MarketBookRunnerClass> runners { get; set; }
    }

    public class MarketBookRunnerClass
    {
        public int selectionId;
        public double handicap;
        public string status;
        public double adjustmentFactor;
        public double lastPriceTraded;
        public double totalMatched;
        public System.DateTime removalDate;
        public Ex ex { get; set; }
        public List<Order> orders { get; set; }
    }

    public class Order
    {
        public string betId;
        public string orderType;
        public string status;
        public string persistenceType;
        public string side;
        public double price;
        public double size;
        public double bspLiability;
        public System.DateTime placedDate;
        public double avgPriceMatched;
        public double sizeMatched;
        public double sizeRemaining;
        public double sizeLapsed;
        public double sizeCancelled;
        public double sizeVoided;
    }

    public class Ex
    {
        public List<AvailableToBack> availableToBack { get; set; }
        public List<AvailableToLay> availableToLay { get; set; }
        public List<TradedVolume> tradedVolume { get; set; }
    }

    public class AvailableToBack
    {
        public double price;
        public double size;
    }

    public class AvailableToLay
    {
        public double price;
        public double size;
    }

    public class TradedVolume
    {
        public double price;
        public double size;
    }

    //classes and function for list orders
    public class ListCurrentOrdersRequest
    {
        public string jsonrpc = "2.0";
        public string method = "SportsAPING/v1.0/listCurrentOrders";
        public ListCurrentOrderParams @params { get; set; }
    }

    public class ListCurrentOrderParams
    {
        public List<string> marketIds { get; set; }
        public List<string> betIds { get; set; }
        public dateRange dateRange { get; set; }
        public string fromRecord = "0";
        public string recordCount = "0";
        public string orderBy = "BY_MARKET";
    }

    public class dateRange
    {
        public string @from;
        public string to;
    }




    public class ListCurrentOrdersResponse
    {

        public string jsonrpc;
        public CurrentOrder result;
        //Public id As Integer = 1

    }

    public class CurrentOrder
    {
        public List<CurrentOrders> currentOrders { get; set; }
    }

    public class CurrentOrders
    {
        public string betId;
        public string marketId;
        public string selectionId;
        public PriceSize priceSize { get; set; }
        public string side;
        public string status;
        public string persistenceType;
        public string orderType;
        public System.DateTime placedDate;
        public double averagePriceMatched;
        public double sizeMatched;
        public double sizeRemaining;
        public double sizeLapsed;
        public double sizeCancelled;

        public double sizeVoided;
    }


    public class PriceSize
    {
        public double price;
        public double size;
    }




    //function and classes for placeorders request
    public class PlaceOrdersRequest
    {
        public string jsonrpc = "2.0";
        public string method = "SportsAPING/v1.0/placeOrders";
        public PlaceOrdersParams @params = new PlaceOrdersParams();
        public int id = 1;
    }

    public class PlaceOrdersParams
    {
        public string marketId;
        public List<PlaceInstructions> instructions = new List<PlaceInstructions>();
    }

    public class PlaceInstructions
    {
        public int selectionId;
        public double handicap;
        public string side;
        //LIMIT is the standard exchange bet for immediate execution
        public string orderType;
        // this defines the price the stake and if the bet perists
        public LimitOrder limitOrder;
    }

    public class LimitOrder
    {
        //stake
        public double size;
        public double price;
        //lapse at event time or continue inrunning 
        public string persistenceType;
    }



    //Classes and functions for placeOrders response - Matched/unmatched orders are reported using these classes
    public class PlaceOrdersResponse
    {
        public string jsonrpc;
        public PlaceExecutionReport result { get; set; }
        public int id;
    }

    public class PlaceExecutionReport
    {
        public string status;
        public string marketId;
        public List<PlaceInstructionReports> instructionReports { get; set; }
    }

    public class PlaceInstructionReports
    {
        public string status;
        public PlaceInstruction instruction { get; set; }
        public string betId;
        public System.DateTime placedDate;
        public double averagePriceMatched;
        public double sizeMatched;
    }

    public class PlaceInstruction
    {
        public int selectionId;
        public double handicap;
        public LimitOrder limitOrder;
        public string orderType;
        public string side;
    }



    //Classes and function for CancelOrders request
    public class CancelOrdersRequest
    {
        public string jsonrpc = "2.0";
        public string method = "SportsAPING/v1.0/cancelOrders";
        public CancelOrdersParams @params { get; set; }
        public int id = 1;
    }

    public class CancelOrdersParams
    {
        public string marketId;
        public List<CancelInstructions> instructions { get; set; }
    }

    public class CancelInstructions
    {
        public string betId;
        public double sizeReduction;
    }



    //classes and function for cancel orders response
    public class CancelOrdersResponse
    {
        public string jsonrpc;
        public CancelExexcutionReport result { get; set; }
        public int id;
    }

    public class CancelExexcutionReport
    {
        public string status;
        public string marketId;
        public List<CancelInstructionReport> instructionReports { get; set; }
    }

    public class CancelInstructionReport
    {
        public string status;
        public CancelInstruction instruction { get; set; }

        public double sizeCancelled;
    }

    public class CancelInstruction
    {
        public string betId;
        public double sizeReduction;
    }



    //classes and function for replace orders request
    public class ReplaceOrdersRequest
    {
        public string jsonrpc = "2.0";
        public string method = "SportsAPING/v1.0/replaceOrders";
        public ReplaceOrdersParams @params { get; set; }
        public int id = 1;
    }

    public class ReplaceOrdersParams
    {
        public string marketId;
        public List<ReplaceInstructions> instructions { get; set; }
        public class ReplaceInstructions
        {
            public string betId;
            public double newPrice;
        }



        //Classes and function for ReplaceOrdersResponse
        public class ReplaceOrdersResponse
        {
            public string jsonrpc;
            public ReplaceExecutionReport result { get; set; }
            public int id;
        }

        public class ReplaceExecutionReport
        {
            public string status;
            public string marketId;
            public List<ReplaceInstructionReport> instructionReports { get; set; }
        }

        public class ReplaceInstructionReport
        {
            public string status;
            public CancelInstructionRep cancelInstructionReport { get; set; }
            public PlaceInstructionRep placeInstructionReport { get; set; }
        }

        public class CancelInstructionRep
        {
            public string status;
            public CancelInstruct instruction { get; set; }
            public double sizeCancelled;
        }
        public class CancelInstruct
        {
            public string betId;
        }

        public class PlaceInstructionRep
        {
            public string status;
            public PlaceInstruct instruction { get; set; }
            public string betId;
            public System.DateTime placeDate;
            public double averagePriceMatched;
            public double sizeMatched;
            public double sizeReduction;
        }
        public class PlaceInstruct
        {
            public int selection;
            public LimitOrder limitOrder { get; set; }
            public string orderType;
            public string size;
        }




    }

}