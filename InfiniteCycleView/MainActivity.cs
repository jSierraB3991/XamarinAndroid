namespace InfiniteCycleView
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Widget;
    using Com.Gigamole.Infinitecycleviewpager;
    using EDMTDialog;
    using InfiniteCycleView.Adapter;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private HorizontalInfiniteCycleViewPager infiniteCycleViewPager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            infiniteCycleViewPager = FindViewById<HorizontalInfiniteCycleViewPager>(Resource.Id.pager_main);
            GetData();
        }

        private async Task GetData()
        {
            var service = new Service.MyALService();
            Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                .SetContext(this)
                .SetMessage(GetString(Resource.String.msg_please_wait)).Build();
            dialog.Show();
            var response = await service.GetFavoritesByUser(this, "Judasibe3991");
            if (!response.IsSuccess)
            {
                dialog.Dismiss();
                Toast.MakeText(this, response.Message, ToastLength.Short).Show();
                return;
            }
            Toast.MakeText(this, response.Result[0].Name, ToastLength.Short).Show();
            LoadData(response.Result);
            dialog.Dismiss();
        }

        public void LoadData(List<Model.Model> data)
        {
            infiniteCycleViewPager.Adapter = new AnimePagerAdapter(this, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}