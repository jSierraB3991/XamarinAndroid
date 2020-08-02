using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace MultiCompleteAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            string[] suggestion = {
                "Android", "C#" ,"Xamarin", "Apache", "Sql Server", "Mysql",
                "Visual Studio", "Visual Studio Code", "SqLite", "Azure", "Amazon Web Services",
                "Basic", "C++"
            };

            var multicomplete = FindViewById<MultiAutoCompleteTextView>(Resource.Id.multiComplete);
            multicomplete.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, suggestion);
            multicomplete.Threshold = 1;
            multicomplete.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}