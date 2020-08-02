using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace SwicthBetweenActivities
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

            var eName = FindViewById<EditText>(Resource.Id.editTextName);
            var eEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            var btnSend = FindViewById<Button>(Resource.Id.btnSend);
            btnSend.Click += (s, e) => {
                var intent = new Intent(this, typeof(RecvActivity));
                intent.PutExtra("name", eName.Text);
                intent.PutExtra("email", eEmail.Text);
                StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}