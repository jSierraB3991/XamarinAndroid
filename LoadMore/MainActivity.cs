namespace LoadMore
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V4.Widget;
    using Android.Support.V7.App;
    using Android.Support.V7.Widget;
    using Android.Widget;
    using EDMTDialog;
    using LoadMore.Adapter;
    using LoadMore.Service;
    using System.Threading.Tasks;
    using static Android.Support.V4.Widget.SwipeRefreshLayout;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnRefreshListener
    {
        private RecyclerView _recyclerView;
        private SwipeRefreshLayout _swipeRefresh;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            _swipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_main);
            _swipeRefresh.SetColorScheme(Resource.Color.colorPrimary,
                Android.Resource.Color.HoloGreenDark,
                Android.Resource.Color.HoloBlueDark,
                Android.Resource.Color.HoloOrangeDark);
            _swipeRefresh.SetOnRefreshListener(this);
            _swipeRefresh.Post(() => { GetData(); });
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_waifu);
            _recyclerView.HasFixedSize = true;
            _recyclerView.SetLayoutManager(new GridLayoutManager(this, 2));
        }

        public async Task GetData()
        {
            var service = new SocialMediaService();
            Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                .SetContext(this).SetMessage(GetString(Resource.String.msg_please_wait)).Build();
            if (!_swipeRefresh.Refreshing)
                dialog.Show();

            var result = await service.GetData(this, 1);
            if (!result.IsSuccess)
            {
                if (!_swipeRefresh.Refreshing)
                    dialog.Dismiss();
                _swipeRefresh.Refreshing = false;
                Toast.MakeText(this, result.Message, ToastLength.Short).Show();
                return;
            }
            _recyclerView.SetAdapter(new WaifuPostAdapter(this, result.Result.Data));
            if (!_swipeRefresh.Refreshing)
                dialog.Dismiss();
            _swipeRefresh.Refreshing = false;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async void OnRefresh()
        {
            await GetData();
        }
    }
}