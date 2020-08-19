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
using Com.Pushlink.Android;
using Plugin.CurrentActivity;

namespace XamarinPushLinkSampleApp
{
    //You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        // private AtlasPreferences
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            SetupPushLink();
            //If AzureMobileServices is being used uncomment the following line of code
            // Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
        }

        private void SetupPushLink()
        {
            var deviceId = Android.Provider.Settings.Secure.GetString(ContentResolver,
                    Android.Provider.Settings.Secure.AndroidId);

            PushLink.SetCurrentStrategy(StrategyEnum.Custom);
            PushLink.Start(this, Resource.Drawable.icon, "PUSH_LINK_API_KEY_GOES_HERE", deviceId);

            RegisterReceiver(new PushLinkUpdateReceiver(), new IntentFilter(Context.PackageName + ".pushlink.APPLY"));
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}