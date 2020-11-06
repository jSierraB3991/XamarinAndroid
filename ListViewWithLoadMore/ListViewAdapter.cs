namespace ListViewWithLoadMore
{
    using Android.Content;
    using Android.Views;
    using Android.Widget;
    using System.Collections.Generic;

    public class ListViewAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly List<string> data;

        public ListViewAdapter(Context context, List<string> data)
        {
            this.context = context;
            this.data = data;
        }

        public override int Count => this.data.Count;

        public override Java.Lang.Object GetItem(int position) => this.data[position];

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ??
                ((LayoutInflater)context.GetSystemService(Context.LayoutInflaterService))
                .Inflate(Resource.Layout.list_item_layotu, null);

            var txtxName = view.FindViewById<TextView>(Resource.Id.txt_main);
            txtxName.Text = data[position];
            return view;
        }
    }
}