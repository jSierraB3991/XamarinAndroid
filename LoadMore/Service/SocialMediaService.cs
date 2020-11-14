namespace LoadMore.Service
{
    using Android.Content;
    using Android.Net;
    using LoadMore.Model;
    using Refit;
    using System.Threading.Tasks;

    internal class SocialMediaService
    {
        private ISocialMediaService mediaService;

        public async Task<Response<RequestPublish>> GetData(Context ctx, int pageNumber)
        {
            if (!IsConnectedToInternet(ctx))
                return new Response<RequestPublish> { IsSuccess = false, Message = ctx.GetString(Resource.String.msg_no_internet_conection) };

            try {
                mediaService = RestService.For<ISocialMediaService>(ctx.GetString(Resource.String.resource_url_api));
                var result = await mediaService.GetPublish(pageNumber);
                return new Response<RequestPublish> { IsSuccess = true, Result = result };
            }
            catch (System.Exception ex) {
                return new Response<RequestPublish> { IsSuccess = false, Message = ex.Message };
            }
        }
        internal static bool IsConnectedToInternet(Context context)
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