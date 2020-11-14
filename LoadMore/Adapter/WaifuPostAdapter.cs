using Android.Content;
namespace LoadMore.Adapter
{
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;
    using LoadMore.Model;
    using Square.Picasso;
    using System;
    using System.Collections.Generic;

    internal class WaifuPostAdapter : RecyclerView.Adapter
    {
        private readonly Context _ctx;
        public List<Publish> _data;

        public WaifuPostAdapter(Context ctx, List<Publish> data)
        {
            this._ctx = ctx;
            this._data = data;
        }

        public override int ItemCount => _data.Count;

        public void AddAllWaifu(List<Publish> waifus)
        {
            waifus.ForEach(Add);
        }

        public int GetLastItemId() 
        {
            return _data[_data.Count - 1].Id;
        }
        private void Add(Publish item)
        {
            _data.Add(item);
            NotifyItemInserted(_data.Count - 1);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var waifuHolader = holder as WaifuHolder;
            Picasso.Get().Load(_data[position].Image).Into(waifuHolader.ImageWaifu);
            waifuHolader.NameText.Text = _data[position].Description.Substring(0, 20);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(_ctx).Inflate(Resource.Layout.waifu_item_layout, parent, false);
            return new WaifuHolder(itemView);
        }
    }

    internal class WaifuHolder : RecyclerView.ViewHolder
    {
        public ImageView ImageWaifu { get; set; }

        public TextView NameText { get; set; }

        public WaifuHolder(View itemView) : base(itemView)
        {
            ImageWaifu = itemView.FindViewById<ImageView>(Resource.Id.image_waifu_item);
            NameText = itemView.FindViewById<TextView>(Resource.Id.text_waifu_name);
        }
    }
}