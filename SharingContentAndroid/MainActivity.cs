using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Content;

namespace SharingContentAndroid
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
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.share_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_share:
                    Intent sharing = new Intent(Intent.ActionSend);
                    sharing.SetType("text/plain");
                    sharing.PutExtra(Intent.ExtraSubject, "Subject Here");
                    sharing.PutExtra(Intent.ExtraText, "This is content to share");
                    sharing.PutExtra(Intent.ExtraTitle, "Title Here");
                    StartActivity(Intent.CreateChooser(sharing, "Shearing Oprions"));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}