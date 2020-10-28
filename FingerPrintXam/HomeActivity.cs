namespace FingerPrintXam
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;

    [Activity(Label = "HomeActivity", Theme = "@style/AppTheme")]
    public class HomeActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);
        }
    }
}