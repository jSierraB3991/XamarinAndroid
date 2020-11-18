namespace PokeDex.Holder
{
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;

    public class PokeDexHolder: RecyclerView.ViewHolder
    {
        public ImageView ImgPrincipal { get; set; }

        public TextView TxtName { get; set; }

        public PokeDexHolder(View view): base(view)
        {
            TxtName = view.FindViewById<TextView>(Resource.Id.textViewPokeName);
            ImgPrincipal = view.FindViewById<ImageView>(Resource.Id.imageViewPrincipal);
        }
    }
}