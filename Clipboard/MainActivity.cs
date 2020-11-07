namespace Clipboard
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Runtime;
    using Android.Widget;
    using Android.Content;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ClipboardManager clipboardManager;
        private ClipData clipData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var btnCopy = FindViewById<Button>(Resource.Id.btn_copy);
            var btnPaste = FindViewById<Button>(Resource.Id.btn_paste);

            var txtCopy = FindViewById<EditText>(Resource.Id.txt_copy);
            var txtPaste = FindViewById<TextView> (Resource.Id.txt_paste);

            clipboardManager = (ClipboardManager)GetSystemService(ClipboardService);
            btnCopy.Click += delegate {
                var copyText = txtCopy.Text;
                clipData = ClipData.NewPlainText("text", copyText);
                clipboardManager.PrimaryClip = clipData;
                Toast.MakeText(this, Resource.String.msg_copy_text, ToastLength.Short).Show();
            };
            btnPaste.Click += delegate {
                ClipData.Item item = clipboardManager.PrimaryClip.GetItemAt(0);
                txtPaste.Text = item.Text;
                Toast.MakeText(this, Resource.String.msg_paste_text, ToastLength.Short).Show();
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}