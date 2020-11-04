namespace ExpandibleListView
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Runtime;
    using Android.Widget;
    using System.Collections.Generic;
    using Java.Sql;
    using System;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ExpandableListAdapter expAdapter;
        private ExpandableListView expandableList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.main_toolbar);
            SetSupportActionBar(toolbar);
            toolbar.Title = "Expandable List View";

            expandableList = FindViewById<ExpandableListView>(Resource.Id.main_expandible);
            SetData();
            expandableList.SetAdapter(expAdapter);
        }

        private void SetData()
        {
            List<string> groupA = new List<string> {
                "A-1", "A-2", "A-3"
            };
            List<string> groupB = new List<string> {
                "B-1", "B-2", "B-3"
            };

            List<string> group = new List<string> { "Group A", "Group B" };
            Dictionary<string, List<string>> dicChild = new Dictionary<string, List<string>>
            {
                {group[0], groupA},
                {group[1], groupB},
            };
            expAdapter = new ExpandableListAdapter(this, group, dicChild);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}