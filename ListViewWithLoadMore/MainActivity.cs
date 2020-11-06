namespace ListViewWithLoadMore
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Runtime;
    using Android.Widget;
    using System.Collections.Generic;
    using System;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView listViewData;
        private readonly List<string> lstSource = new List<string>();
        private int maxPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_main);
            toolbar.Title = GetString(Resource.String.app_name);
            SetSupportActionBar(toolbar);

            listViewData = FindViewById<ListView>(Resource.Id.list_view_main);
            LoadData();
            maxPosition = lstSource.Count;
            var buttonLoadMore = new Button(this) { Text = GetString(Resource.String.txt_load_more) };
            listViewData.Adapter = new ListViewAdapter(this, lstSource);
            buttonLoadMore.Click += ButtonLoadMore_Click;
            listViewData.AddFooterView(buttonLoadMore);

        }

        private void ButtonLoadMore_Click(object sender, EventArgs e)
        {
            listViewData.SetSelection(maxPosition);
            RunOnUiThread(() => {
                for (int i = maxPosition; i < maxPosition + 20; i++)
                    lstSource.Add($"List Item {i}");
                listViewData.SetSelectionFromTop(listViewData.FirstVisiblePosition + 1, 0);
                listViewData.Adapter = new ListViewAdapter(this, lstSource);
                listViewData.SetSelection(maxPosition);
                maxPosition = lstSource.Count;
            });
            
        }

        private void LoadData()
        {
            for (int i = 0; i < 20; i++) lstSource.Add($"List Item {i}");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}