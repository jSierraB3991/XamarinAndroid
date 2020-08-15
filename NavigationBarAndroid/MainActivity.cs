using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace NavigationBarAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView txtTextView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            txtTextView = FindViewById<TextView>(Resource.Id.txtTextView);

            var btnHidden = FindViewById<Button>(Resource.Id.bntHidden);
            var btnVisible = FindViewById<Button>(Resource.Id.btnVisible);
            var bntLowProfile = FindViewById<Button>(Resource.Id.btnLowProfile);

            txtTextView.SystemUiVisibilityChange += (s, e) => { txtTextView.Text = $"Visible: {e.Visibility}"; };

            bntLowProfile.Click += (s, e) => { txtTextView.SystemUiVisibility = (StatusBarVisibility)Android.Views.SystemUiFlags.LowProfile; };
            btnHidden.Click += (s, e) => { txtTextView.SystemUiVisibility = (StatusBarVisibility)Android.Views.SystemUiFlags.HideNavigation; };
            btnVisible.Click += (s, e) => { txtTextView.SystemUiVisibility = (StatusBarVisibility)Android.Views.SystemUiFlags.Visible; };
        }
    }
}