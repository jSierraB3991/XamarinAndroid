namespace SqlLiteAndroid
{
    using Android.App;
    using Android.Graphics;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Widget;
    using SqlLiteAndroid.Adapter;
    using SqlLiteAndroid.Data;
    using SqlLiteAndroid.Model;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView listView;
        private TextView name, email, age;
        private Button add, update, delete;
        private DatabaseLite db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            CreateDatabase();
            SetContext();
            LoadData();
            listView.ItemClick += (sender, e) =>
            {
                for (int i = 0; i < listView.Count; i++)
                {
                    if (e.Position == i)
                        listView.GetChildAt(i).SetBackgroundColor(Color.DarkGray);
                    else
                        listView.GetChildAt(i).SetBackgroundColor(Color.Transparent);
                }
                var aux = e.View.FindViewById<TextView>(Resource.Id.tvName);
                name.Text = e.View.FindViewById<TextView>(Resource.Id.tvName).Text;
                name.Tag = e.Id;
                age.Text = e.View.FindViewById<TextView>(Resource.Id.tvAge).Text;
                email.Text = e.View.FindViewById<TextView>(Resource.Id.tvEmail).Text;
            };
        }

        private void LoadData()
        {
            var ls = db.SelectTableItems();
            var adapter = new ListViewAdapter(this, ls);
            listView.Adapter = adapter;
        }

        private void CreateDatabase()
        {
            db = new DatabaseLite();
            db.CreateDatabase();
        }

        private void SetContext()
        {
            listView = FindViewById<ListView>(Resource.Id.listData);

            name = FindViewById<TextView>(Resource.Id.txtName);
            age = FindViewById<TextView>(Resource.Id.txtAge);
            email = FindViewById<TextView>(Resource.Id.txtEmail);

            add = FindViewById<Button>(Resource.Id.btnAdd);
            update = FindViewById<Button>(Resource.Id.btnUpdate);
            delete = FindViewById<Button>(Resource.Id.btnDelete);

            add.Click += (sender, e) =>
            {
                if (!ValidarData()) return;
                var item = new Item { Age = age.Text, Email = email.Text, Name = name.Text };
                db.InserItem(item);
                LoadData();
                CleanData();
            };
            update.Click += (sender, e) =>
            {
                if (!ValidarData()) return;
                if (ValideId()) return;

                var item = new Item
                {
                    Age = age.Text,
                    Email = email.Text,
                    Name = name.Text,
                    Id = int.Parse(name.Tag.ToString())
                };
                db.UpdateItem(item);
                name.Tag = string.Empty;
                LoadData();
                CleanData();
            };
            delete.Click += (sender, e) =>
            {
                if (!ValidarData()) return;
                if (ValideId())
                    return;
                var item = new Item
                {
                    Age = age.Text,
                    Email = email.Text,
                    Name = name.Text,
                    Id = int.Parse(name.Tag.ToString())
                };
                db.DeleteIem(item);
                name.Tag = string.Empty;
                LoadData();
                CleanData();
            };
        }

        private bool ValideId()
        {
            if (name.Tag == null || string.IsNullOrEmpty(name.Tag.ToString()))
            {
                Toast.MakeText(this, Resource.String.msg_not_select, ToastLength.Short).Show();
                return true;
            }
            return false;
        }

        private bool ValidarData()
        {
            if (string.IsNullOrEmpty(name.Text) ||
                string.IsNullOrEmpty(age.Text) ||
                string.IsNullOrEmpty(email.Text))
            {
                Toast.MakeText(this, Resource.String.msg_all_field, ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private void CleanData() { name.Text = age.Text = email.Text = string.Empty; }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}