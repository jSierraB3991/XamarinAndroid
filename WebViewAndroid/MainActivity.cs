using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Webkit;
using Android.Widget;

namespace WebViewAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            EditText txtUrl = FindViewById<EditText>(Resource.Id.txtUrl);

            WebView webView = FindViewById<WebView>(Resource.Id.webView);
            webView.SetWebViewClient(new ExtendWebViewClient());

            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;

            var btnGo = FindViewById<Button>(Resource.Id.btnGo);
            btnGo.Click += (s, e) => {

                string addres = txtUrl.Text.ToLower().Replace(" ", string.Empty);
                if (!addres.StartsWith("https://")) txtUrl.Text = $"https://{addres}";

                webView.LoadUrl(txtUrl.Text);
            };
        }
    }

    internal class ExtendWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}