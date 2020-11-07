namespace DownloadFileProgress
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Runtime;
    using Android.Widget;
    using Android.Net;
    using Android.Content;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var btnMain = FindViewById<Button>(Resource.Id.btn_main);
            var imageView = FindViewById<ImageView>(Resource.Id.imageview_main);
            btnMain.Click += delegate {

                ConnectivityManager manager = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
                NetworkInfo activeInfo = manager.ActiveNetworkInfo;
                if (activeInfo != null && activeInfo.IsConnected)
                {
                    if (activeInfo.Type == ConnectivityType.Wifi)
                        Toast.MakeText(this, Resource.String.msg_dowload_by_wifi, ToastLength.Short).Show();
                    else if (activeInfo.Type == ConnectivityType.Mobile)
                        Toast.MakeText(this, Resource.String.msg_dowload_by_data, ToastLength.Short).Show();

                    var download = new DownloadImageFromUrl(this, imageView);
                    download.Execute("https://images.dog.ceo/breeds/bullterrier-staffordshire/n02093256_8623.jpg");
                }
            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}