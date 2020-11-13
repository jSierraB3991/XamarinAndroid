namespace InfiniteCycleView.Service
{
    using Android.Content;
    using Android.Net;

    public class Util
    {
        public static bool IsConnectedToInternet(Context context)
        {
            ConnectivityManager manager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            if (manager != null)
            {
                NetworkInfo[] info = manager.GetAllNetworkInfo();
                if (info != null)
                {
                    foreach (var item in info)
                    {
                        if (item.GetState() == NetworkInfo.State.Connected) return true;
                    }
                }
            }
            return false;
        }
    }
}