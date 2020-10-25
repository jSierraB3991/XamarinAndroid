namespace XamSplashGif
{
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Widget;
    using Felipecsl.GifImageViewLibrary;
    using System.IO;
    using System.Timers;

    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme")]
    public class SplashActivity : AppCompatActivity
    {
        private GifImageView gifImageView;
        private ProgressBar progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashScreen);

            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageView);
            progressBar = FindViewById<ProgressBar>(Resource.Id.pgRefresh);

            var input = Assets.Open("splash_screen.gif");
            var bytes = ConvertFileToByteArray(input);
            gifImageView.SetBytes(bytes);
            gifImageView.StartAnimation();

            Timer timer = new Timer
            {
                Enabled = true,
                Interval = 3000,
                AutoReset = false
            };
            timer.Elapsed += (sender, e) => {
                StartActivity(new Intent(this, typeof(MainActivity)));
            };
        }

        private byte[] ConvertFileToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream()) 
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}