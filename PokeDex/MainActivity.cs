namespace PokeDex
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Runtime;
    using PokeDex.Service;
    using System;
    using Android.Support.V4.Widget;
    using Android.Support.V7.Widget;
    using Android.Widget;
    using PokeDex.Adapters;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private SwipeRefreshLayout _refreshPokeList;
        private RecyclerView _recyclerListPokeDex;
        private TextView _txtCount;
        private string _url = "https://pokeapi.co/api/v2/pokemon?offset=0&limit=21";

        public MainActivity()
        {
            new DependencesService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            _refreshPokeList = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            _recyclerListPokeDex = FindViewById<RecyclerView>(Resource.Id.recyclerViewPoke);
            _txtCount = FindViewById<TextView>(Resource.Id.textViewCount);
            _refreshPokeList.Refresh += (sender, e) => {
                this.GetPokemons();
            };
            GetPokemons();
        }

        private async void GetPokemons()
        {
            if (string.IsNullOrEmpty(this._url)) {
                Toast.MakeText(this, "Reinicio", ToastLength.Short).Show(); 
                this._url = "https://pokeapi.co/api/v2/pokemon?offset=0&limit=21";
            }
            this._refreshPokeList.Refreshing = true;
            var data = await DependencesService.INSTANCE.PokeDexApiService.GetAll(this._url);
            if (!data.IsSuccess) { 
                this._refreshPokeList.Refreshing = false;
                Toast.MakeText(this, data.Message, ToastLength.Short).Show();
                return;
            }
            this._url = data.Result.Next;
            this._txtCount.Text = $"All Pokemons is: {data.Result.Count}";
            this._recyclerListPokeDex = FindViewById<RecyclerView>(Resource.Id.recyclerViewPoke);
            GridLayoutManager layoutManager = new GridLayoutManager(this, 3);
            DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(this, layoutManager.Orientation);
            this._recyclerListPokeDex.SetAdapter(new PokeDexAdapter(this, data.Result.Results));
            this._recyclerListPokeDex.SetLayoutManager(layoutManager);
            this._recyclerListPokeDex.AddItemDecoration(dividerItemDecoration);
            this._refreshPokeList.Refreshing = false;
        }

        public override void OnRequestPermissionsResult(int requestCode, 
                                                        string[] permissions, 
                                                        [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}