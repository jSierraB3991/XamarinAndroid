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
    using LoadMore.Listener;
    using LoadMore.Service;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Android.Support.V4.Widget.SwipeRefreshLayout;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnRefreshListener
    {
        private int CurrentPage = 1;
        private int TotalPage = 0;
        private RecyclerView _recyclerView;
        private SwipeRefreshLayout _swipeRefresh;
        private WaifuPostAdapter waifuAdapter;
        public GridLayoutManager gridLayoutManager;
        private bool IsLoading = false;

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
            _swipeRefresh.Post(() => { SeedData(); });
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_waifu);
            _recyclerView.HasFixedSize = true;
            gridLayoutManager = new GridLayoutManager(this, 2);
            _recyclerView.SetLayoutManager(gridLayoutManager);
            _recyclerView.AddOnScrollListener(new LoadMoreListener(this));
        }

        internal async Task SeedData()
        {
            var data = await GetData();
            waifuAdapter = new WaifuPostAdapter(this, data);
            _recyclerView.SetAdapter(waifuAdapter);
        }

        internal async Task<List<Model.Publish>> GetData()
        {
            var service = new SocialMediaService();
            Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                .SetContext(this).SetMessage(GetString(Resource.String.msg_please_wait)).Build();
            if (!_swipeRefresh.Refreshing)
                dialog.Show();

            var result = await service.GetData(this, CurrentPage);
            TotalPage = result.Result.MetaData.TotalPages;
            if (!result.IsSuccess)
            {
                if (!_swipeRefresh.Refreshing)
                    dialog.Dismiss();
                _swipeRefresh.Refreshing = false;
                Toast.MakeText(this, result.Message, ToastLength.Short).Show();
                return null;
            }

            if (!_swipeRefresh.Refreshing)
                dialog.Dismiss();
            _swipeRefresh.Refreshing = false;
            return result.Result.Data;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async void OnRefresh()
        {
            CurrentPage = 1;
            await SeedData();
        }

        public async void GetMoreData()
        {
            if (CurrentPage >= TotalPage) return;
            if (IsLoading) return;
            IsLoading = true;
            CurrentPage++;
            await AddData();
        }

        private async Task AddData()
        {
            var data = await GetData();
            waifuAdapter.AddAllWaifu(data);
            IsLoading = false;
        }
    }
}