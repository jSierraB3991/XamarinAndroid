namespace SwipeRecyclerView.Adapter
{
    using Android.Content;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Com.Bumptech.Glide;
    using SwipeRecyclerView.Holder;
    using SwipeRecyclerView.Model;
    using System.Collections.Generic;

    public class ItemAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;
        private readonly List<Item> _items;

        public ItemAdapter(Context context, List<Item> items)
        {
            this._context = context;
            this._items = items;
        }

        public override int ItemCount => _items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var myViewHolder = holder as MyViewHolder;
            Glide.With(this._context)
                .Load(_items[position].Image)
                .Into(myViewHolder.Image);
            myViewHolder.TxtName.Text = _items[position].Name;
            myViewHolder.TxtPrice.Text = _items[position].Price;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemVIew = LayoutInflater.From(this._context).Inflate(Resource.Layout.item_layout, parent, false);
            return new MyViewHolder(itemVIew);
        }
    }
}