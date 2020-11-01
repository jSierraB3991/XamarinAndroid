namespace SwipeRecyclerView.Holder
{
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;

    public class MyViewHolder: RecyclerView.ViewHolder
    {
        public TextView TxtName { get; }

        public TextView TxtPrice { get; }

        public ImageView Image { get; }


        public MyViewHolder(View viewItem): base(viewItem)
        {
            this.TxtName = ItemView.FindViewById<TextView>(Resource.Id.txtName);
            this.TxtPrice = ItemView.FindViewById<TextView>(Resource.Id.txtPrice);
            this.Image = ItemView.FindViewById<ImageView>(Resource.Id.cart_image);
        }
    }
}