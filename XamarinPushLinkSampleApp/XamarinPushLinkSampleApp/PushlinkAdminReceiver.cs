using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Runtime;
using Android.Util;

namespace XamarinPushLinkSampleApp
{
    [Preserve(AllMembers = true)]
    [BroadcastReceiver(Permission = "android.permission.BIND_DEVICE_ADMIN", Enabled = true, Name = "com.xenex.xamarinpushlinksampleapp.PushlinkAdminReceiver")]
    [MetaData("android.app.device_admin", Resource = "@xml/device_admin_sample")]
    [IntentFilter(new[] { "android.app.action.DEVICE_ADMIN_ENABLED", Intent.ActionMain })]
    [IntentFilter(new[] { "android.intent.action.MY_PACKAGE_REPLACED", Intent.ActionMain })]
    public class PushlinkAdminReceiver : DeviceAdminReceiver
    {
        public override void OnReceive(Context context, Intent i)
        {
            base.OnReceive(context, i);
            if (i.Action.Equals(Intent.ActionMyPackageReplaced))
            {
                Log.Info("TESTPUSHLINK", "MY_PACKAGE_REPLACED called");
                var restartIntent = new Intent(context, typeof(MainActivity));
                restartIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                context.StartActivity(restartIntent);
            }
            else
            {
                Log.Info("TESTPUSHLINK", "DEVICE_ADMIN_ENABLED called");
            }
        }
    }
}
