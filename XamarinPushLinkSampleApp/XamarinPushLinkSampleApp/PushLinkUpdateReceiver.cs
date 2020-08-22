using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Util;
using Java.IO;
using Java.Lang;
using Exception = System.Exception;

namespace XamarinPushLinkSampleApp
{

    /// <summary>
    /// Handles Pushlink Apply action to execute silent upgrade intall.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class PushLinkUpdateReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Log.Info("TESTPUSHLINK", "pushlink.APPLY called");
            if (IsDeviceOwner(context))
            {
                var apkUri = intent.Extras.Get("uri") as Android.Net.Uri;
                InstallSilently(context, apkUri);
            }
            else
            {
                Log.Info("TESTPUSHLINK", "You are not DEVICE OWNER. Update skipped!");
            }
        }

        private bool IsDeviceOwner(Context context)
        {
            try
            {
                var devicePolicyManager = (DevicePolicyManager)context.GetSystemService(Context.DevicePolicyService);
                return devicePolicyManager != null && devicePolicyManager.IsDeviceOwnerApp(Application.Context.PackageName);
            }
            catch (Exception t)
            {
                Log.Error("TESTPUSHLINK", t.StackTrace);
                return false;
            }
        }

        private void InstallSilently(Context context, Android.Net.Uri apkUri)
        {
            try
            {
                Log.Debug("TESTPUSHLINK", "Init Silent Install. apkUri is " + apkUri.Path + " apkUri host is " + apkUri.Host);
                var inputStream = new FileInputStream(new File(context.FilesDir, apkUri.Path));
                var packageInstaller = context.PackageManager.PackageInstaller;
                var installParams = new PackageInstaller.SessionParams(PackageInstallMode.FullInstall);
                installParams.SetAppPackageName(context.PackageName);
                var sessionId = packageInstaller.CreateSession(installParams);
                var session = packageInstaller.OpenSession(sessionId);
                var outStream = session.OpenWrite("COSU", 0, -1);
                var buffer = new byte[65536];
                int c;

                // HERE THIS LOOP HANGS FOREVER
                while ((c = inputStream.Read(buffer)) != -1)
                {
                    outStream.Write(buffer, 0, c);
                    // Log.Debug("PUSHLINK", "writing..");
                }

                // NEVER COMES OUT
                Log.Debug("TESTPUSHLINK", "Silent Install DONE!! ");

                session.Fsync(outStream);
                inputStream.Close();
                outStream.Close();

                var pendingIntent =
                    PendingIntent.GetBroadcast(context, sessionId, new Intent("dummy.intent.not.used"), 0);
                session.Commit(pendingIntent.IntentSender);
            }
            catch (IOException e)
            {
                Log.Error("TESTPUSHLINK", "Silent Install failed to silent -r install. EX: " + e.GetStackTrace());

                throw new RuntimeException(e);
            }
            catch (Exception e)
            {
                Log.Error("TESTPUSHLINK", "Silent Install failed to silent -r install. EX: " + e.StackTrace);
            }
        }
    }
}
