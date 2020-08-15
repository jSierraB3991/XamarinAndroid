using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace AutoCompleteAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var autoCompleteOptions = new string[] { "Apple", "Red", "Orange", "Blue","Greeen", "Yellow", "Black", "Whitw" };
            ArrayAdapter arrayAdapter = new ArrayAdapter(this, 
                                                         Android.Resource.Layout.SimpleDropDownItem1Line,
                                                         autoCompleteOptions);
            var autoCompleteTextVIew = FindViewById<AutoCompleteTextView>(Resource.Id.autoComplete);
            autoCompleteTextVIew.Adapter = arrayAdapter;
        }
    }
}