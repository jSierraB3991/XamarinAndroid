using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InfiniteCycleView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            GetData();
        }

        private async Task GetData()
        {
            var service = new Service.MyALService();
            var response = await service.GetFavoritesByUser(this, "Judasibe3991");
            if (!response.IsSuccess) {
                Toast.MakeText(this, response.Message, ToastLength.Short).Show();
                return;
            }
            Toast.MakeText(this, response.Result[0].Name, ToastLength.Short).Show();
            LoadData(response.Result);

        }

        public void LoadData(List<Model.Model> data)
        { 
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}