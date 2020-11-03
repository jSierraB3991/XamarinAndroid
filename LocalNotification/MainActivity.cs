namespace LocalNotification
{
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V4.App;
    using Android.Support.V7.App;
    using Android.Widget;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var btnSend = FindViewById<Button>(Resource.Id.btnSendNotification);
            btnSend.Click += (sender, e) => {


                if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                {
                    return;
                }
                var bundle = new Bundle();
                bundle.PutString("sendContent", "This is content send from activity");
                var newItent = new Intent(this, typeof(SecondActivity));
                newItent.PutExtras(bundle);

                var taskStackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                taskStackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(SecondActivity)));
                taskStackBuilder.AddNextIntent(newItent);

                var pending = taskStackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                var builder = new NotificationCompat.Builder(this, "LOCAL")
                                        .SetAutoCancel(true)
                                        .SetContentIntent(pending)
                                        .SetContentTitle("My Local Notification")
                                        .SetSmallIcon(Resource.Drawable.ic_stat_wifi_tethering)
                                        .SetContentText("Click here to next Activity");

                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                notificationManager.Notify(0, builder.Build());
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}