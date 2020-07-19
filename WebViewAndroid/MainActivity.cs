using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Lang;

namespace WebViewAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private WebView webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            EditText txtUrl = FindViewById<EditText>(Resource.Id.txtUrl);

            webView = FindViewById<WebView>(Resource.Id.webView);
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

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && webView.CanGoBack())
            {
                webView.GoBack();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
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