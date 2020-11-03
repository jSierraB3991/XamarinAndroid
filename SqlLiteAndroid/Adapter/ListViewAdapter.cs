namespace SqlLiteAndroid.Adapter
{
    using Android.Content;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Views;
    using Android.Widget;
    using SqlLiteAndroid.Model;
    using System.Collections.Generic;

    public class ListViewAdapter : BaseAdapter
    {

        private readonly AppCompatActivity _context;
        private readonly List<Item> _items;

        public ListViewAdapter(AppCompatActivity context, List<Item> items)
        {
            this._context = context;
            this._items = items;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position) => _items[position].Id;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = _context.LayoutInflater.Inflate(Resource.Layout.item_data, parent, false);
            var name = view.FindViewById<TextView>(Resource.Id.tvName);
            var age = view.FindViewById<TextView>(Resource.Id.tvAge);
            var email = view.FindViewById<TextView>(Resource.Id.tvEmail);

            name.Text = _items[position].Name;
            age.Text = _items[position].Age;
            email.Text = _items[position].Email;

            return view;
        }

        public override int Count => _items.Count;

    }

    public class ListViewAdapterViewHolder : Java.Lang.Object
    {
        public TextView Name { get; set; }

        public TextView Age { get; set; }

        public TextView Email { get; set; }
    }
}