using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace CalendarView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var calendarView = FindViewById<Android.Widget.CalendarView>(Resource.Id.calendarView);
            var txtDisplay = FindViewById<TextView>(Resource.Id.txtDisplay);

            txtDisplay.Text = "Date: ";
            calendarView.DateChange += (s, e) =>
            {
                txtDisplay.Text = $"Date: {e.DayOfMonth}/{e.Month}/{e.Year}";
            };
        }
    }
}