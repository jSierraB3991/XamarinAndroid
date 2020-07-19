using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace AndroidRatingBar
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var ratingBar = FindViewById<RatingBar>(Resource.Id.ratingBar);
            var txtRate = FindViewById<TextView>(Resource.Id.txtRating);

            ratingBar.RatingBarChange += (s, e) => {
                txtRate.Text = $"Rate {e.Rating}";
            };
        }
    }
}