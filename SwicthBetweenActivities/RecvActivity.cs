using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace SwicthBetweenActivities
{
    [Activity(Label = "RecvActivity")]
    public class RecvActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.second_layout);
            var name = Intent.GetStringExtra("name" ?? string.Empty);
            var email = Intent.GetStringExtra("email" ?? string.Empty);

            var tName = FindViewById<TextView>(Resource.Id.textViewName);
            var tEmail = FindViewById<TextView>(Resource.Id.textViewEmail);
            tName.Text = name;
            tEmail.Text = email;


            var btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnBack.Click += (s, e) =>
            {
                Finish();
            };
        }
    }
}