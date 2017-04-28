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

namespace betbrane.Resources.Back_Office_Work
{
    class PricesProcessing
    {

        public static double Bank;
        public class evalRunner
        {
            public MarketBookRunnerClass runner;
            public laySide lay;
            public backSide back;
            public double wofRatio;

        }
        public class laySide
        {
            public double weightOfMoney;//= sum of (pricesmatched * sum of volume matched) / sum of volume matched
            public double weightOfFlow;//
            public double volumeWeightedAveragePrice;
        }

        public class backSide
        {
            public double weightOfMoney;
            public double weightOfFlow;
            public double volumeWeightedAveragePrice;
        }



        MarketBookResponse resp = new MarketBookResponse();
        MarketBookRunnerClass runner = new MarketBookRunnerClass();
        List<MarketBookRunnerClass> runners = new List<MarketBookRunnerClass>();
        List<AvailableToBack> a2b = new List<AvailableToBack>();
        RESTService rs = new RESTService();
        List<Order> orderList = new List<Order>();

        public void ProcessMarketData(double bank, MarketBookResponse resp)
        {
            Bank = bank;
            checkMarket(resp);
            foreach (MarketBookRunnerClass m in resp.result[0].runners) { runners.Add(m); }
            //  foreach(AvailableToLay a in runners.)

            string marketId = resp.result[0].marketId;
            foreach (MarketBookRunnerClass r in resp.result[0].runners)
            {
                if (r.orders != null)
                {
                    var openOrders = r.orders.ToList().Where(o => o.sizeRemaining > 0);
                    var selectionId = r.selectionId;
                    foreach (Order o in openOrders)
                    {
                        double priceMatched = o.price;
                        double priceAvailableToBack = r.ex.availableToBack[0].price;
                        double stakeAmt = o.size;
                        double priceAvailableToLay = r.ex.availableToLay[0].price;
                        double newStake;
                        string side;
                        //Price is now smaller than when originally matched take a profit
                        if (priceAvailableToLay < priceMatched && (priceMatched / priceAvailableToBack) <= .95)
                        {
                            side = "LAY";
                            newStake = Math.Round(((stakeAmt * priceMatched) + stakeAmt) / ((priceAvailableToBack) + 1), 2);
                            PlaceOrdersResponse por = PlaceOrder(selectionId, marketId, side, newStake, priceAvailableToLay);
                            if (por.result.instructionReports[0].sizeMatched != newStake)
                            {
                                CancelOrder(por.result.instructionReports[0].betId, marketId, newStake - por.result.instructionReports[0].sizeMatched);
                            }
                        }
                        //Price is now bigger than when originally matched take a loss
                        if (priceMatched > priceAvailableToBack && (priceMatched / priceAvailableToBack) >= 1.08)
                        {
                            newStake = Math.Round(((stakeAmt * priceMatched) + stakeAmt) / ((priceAvailableToBack) + 1), 2);
                            side = "BACK";
                            double nse = Math.Round(((stakeAmt * priceMatched) + stakeAmt) / ((priceAvailableToBack) + 1), 2);
                            PlaceOrdersResponse por = PlaceOrder(selectionId, marketId, side, newStake, priceAvailableToLay);
                        }
                    }

                }
            }

        }

        private void CancelOrder(string id, string marketId, double reduction)
        {
            CancelOrdersRequest req = new CancelOrdersRequest();
            CancelOrdersParams parms = new CancelOrdersParams();
            CancelInstructions instructions = new CancelInstructions();
            instructions.betId = id;
            instructions.sizeReduction = reduction;
            parms.marketId = marketId;
            parms.instructions.Add(instructions);
            req.@params = parms;
            CancelOrdersResponse resp = JsonConvert.DeserializeObject<CancelOrdersResponse>(rs.Invoke(JsonConvert.SerializeObject(req)));

        }

        private void checkMarket(MarketBookResponse resp)
        {
            foreach (MarketBookRunnerClass r in resp.result[0].runners)
            {
                List<AvailableToBack> ab = r.ex.availableToBack.ToList();
                var abs = r.ex.availableToBack.ToList().Sum(p => p.size);
                var als = r.ex.availableToLay.ToList().Sum(p => p.size);
                var WeightOfMoney = abs / als;
                var Traded = r.ex.tradedVolume;
                if (WeightOfMoney <= .7) { PlaceOrder(r.selectionId, resp.result[0].marketId, "BACK", r.ex.availableToBack[0].price, 2); }
            }
        }

        private PlaceOrdersResponse PlaceOrder(int selectionId, string marketId, string side, double size, double price)
        {
            PlaceOrdersRequest req = new PlaceOrdersRequest();
            PlaceOrdersParams parms = new PlaceOrdersParams();
            parms.marketId = marketId;
            PlaceInstructions instructions = new PlaceInstructions();
            instructions.selectionId = selectionId;
            instructions.side = side;
            instructions.handicap = 0;
            instructions.orderType = "LIMIT";
            LimitOrder lo = new LimitOrder();
            lo.price = price;
            lo.size = size;
            lo.persistenceType = "LAPSE";
            instructions.limitOrder = lo;

            parms.instructions.Add(instructions);
            req.@params = parms;

            // instructions.limitOrder = "LIMIT";
            PlaceOrdersResponse pr = JsonConvert.DeserializeObject<PlaceOrdersResponse>(rs.Invoke(JsonConvert.SerializeObject(req)));

            return pr;

        }




        //private double calcWOM(MarketBookRunnerClass e)
        //{
        //    var a2b =  e.ex.availableToBack.ToList();
        //    foreach (AvailableToBack a in e.ex.availableToBack){a2b.Add(a.size);}
        //    return Sum
        //}


    }
}