using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V7.Widget;
using System;
using SwipeRecyclerView.Model;
using System.Collections.Generic;
using SwipeRecyclerView.Adapter;

namespace SwipeRecyclerView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            this.recyclerView = FindViewById<RecyclerView>(Resource.Id.recycleView);
            this.recyclerView.HasFixedSize = true;
            LinearLayoutManager layout = new LinearLayoutManager(this);
            this.recyclerView.SetLayoutManager(layout);

            GenerateItems();
        }

        private void GenerateItems()
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < 50; i++)
            {
                var item = new Item {
                    Name = $"Pie {i}",
                    Price = "100.000$",
                    Image = "https://assets.tmecosys.com/image/upload/t_web600x528/img/recipe/ras/Assets/4ADF5D92-29D0-4EB7-8C8B-5C7DAA0DA74A/Derivates/92b597e3-0701-4246-8ca4-4c992ff4cd44.jpg"
                };

                items.Add(item);
            }
            ItemAdapter ItemAdapter = new ItemAdapter(this, items);
            recyclerView.SetAdapter(ItemAdapter);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}