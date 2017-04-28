using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using betbrane.Resources.Activities;
using System.Threading;
using System;
using Android.Views;
using Android;

namespace betbrane
{
    [Activity(Label = "betbrane", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static  string username;
        protected override void OnCreate(Bundle bundle)
        {   
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            StartActivity(typeof(ViewSportsEvents));
            var ùserNameTextBox = FindViewById<EditText>(Resource.Id.textUserName);
            var userNameTextView = FindViewById<TextView>(Resource.Id.textUserName);
            var passwordTextBox = FindViewById<EditText>(Resource.Id.textPassword);
            var passwordTextView = FindViewById<TextView>(Resource.Id.textPassword);
            var displayUserNameText = FindViewById<TextView>(Resource.Id.textDisplay);
            var logInButton = FindViewById<Button>(Resource.Id.logIn);

            ùserNameTextBox.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
              displayUserNameText.Text = e.Text.ToString();
                username = e.Text.ToString();
            };

            if (userNameTextView.Text != null && passwordTextBox.Text != null) {
                logInButton.Enabled = true;
                
            }

            logInButton.Click += (sender, e) =>
            {
                var myProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
                myProgressBar.Visibility = ViewStates.Visible;
                username = displayUserNameText.Text;
                var betfairmesage = FindViewById<TextView>(Resource.Id.textDisplayBetfairMessage);
                betfairmesage.Text = "Connecting to Betfair Server";
                Thread t1 = new Thread(new ThreadStart(Sleep));
                t1.Start();
              
            };

        }

         void Sleep()
        {
            Thread.Sleep(0);
            
            var intent = new Intent(this, typeof(ViewSportsEvents));
            intent.PutExtra("UserName", username);
            StartActivity(intent);
           // myProgressBar.Visibility = ViewStates.Gone;
        }


       
    }
}

