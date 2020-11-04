namespace ImageSlider
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.main_toolbar);
            SetSupportActionBar(toolbar);
            toolbar.Title = "Image Slider";
            var viewPager= FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.main_view_pager);
            viewPager.Adapter = new ImageAdapter(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}