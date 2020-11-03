namespace LocalNotification
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Widget;

    [Activity(Label = "Second Activity")]
    public class SecondActivity: AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.second_layout);
            var sendContext = Intent.Extras.GetString("sendContent");
            var text = FindViewById<TextView>(Resource.Id.txtMessage);
            text.Text = sendContext;
        }
    }
}