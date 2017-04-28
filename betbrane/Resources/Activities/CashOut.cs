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
using Android;

namespace betbrane.Resources.Activities
{
    [Activity(Label = "CashOut")]
    public class CashOut : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CashOut);

            string cashOut = (Intent.GetStringExtra("key") ?? "Data not available");
            var cashOutView = FindViewById<TextView>(Resource.Id.textDisplayProfit);
            var cashoutButton = FindViewById<Button>(Resource.Id.cashOutButton);
            cashoutButton.Text = "You Have Cashed Out For" + " " + cashOut;
            cashoutButton.Click +=(sender,e) => {
                Intent intent = new Intent(this, typeof(ViewEvents));
                StartActivity(intent);
                };

        }
    }
}