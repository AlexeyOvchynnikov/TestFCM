using System;
using Android.Gms.Common;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.OS;

namespace TestFCM.Droid
{
    [Activity(Label = "TestFCM", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Context;
        private const string Tag = "MainActivity";
        protected override void OnCreate(Bundle bundle)
        {
           
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Context = this;
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            IsPlayServicesAvailable();
        }
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Log.Debug(Tag, "GoogleApiAvailability:" + GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Log.Debug(Tag, "GoogleApiAvailability:" + "This device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                Log.Debug(Tag, "GoogleApiAvailability:" + "Google Play Services is available.");
                return true;
            }
        }

        public void ButtonClick()
        {
            Log.Debug(Tag, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
        }
    }
}

