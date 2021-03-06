using Android.App;
using Android.Content.PM;
using Android.OS;

namespace XAndroidWeather
{
    [Activity(
       Theme = "@style/MyTheme.Splash",
       MainLauncher = true,
       NoHistory = true, 
       ConfigurationChanges = ConfigChanges.ScreenSize, 
       ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}