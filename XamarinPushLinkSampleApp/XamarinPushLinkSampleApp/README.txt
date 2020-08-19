Xamarin.Android PushLink Sample Application using CUSTOM strategy for silent upgrade installs
______________________________________________________________________________________________

TODO:// SILENT INSTALLS BEGIN BUT NEVER COMPLETE

NOTE: if running update MainApplication.cs to use you pushlink api key.
In order for silent updates to work we need a rooted Android device and must give Sample app DEVICE_OWNER policy permission:
	run SU 'dpm set-device-owner com.xenex.xamarinpushlinksampleapp/.PushlinkAdminReceiver' to grant perm after installing application.


WARNING: Once DEVICE_OWNER permission is granted there is no way to unset but to factory reset the device.

REFERENCES:
https://pushlink.gitbook.io/docs/android-7-and-8
https://github.com/pushlink/pushlink-mono-sample
https://github.com/pushlink/background-device-owner]


PushLinkMono.dll downloaded @ https://s3.amazonaws.com/bin.pushlink.com/PushLinkMono-5.5.3.dll
