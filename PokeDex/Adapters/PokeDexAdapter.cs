namespace PokeDex.Adapters
{
    using Android.Content;
    using Android.Support.V7.Widget;
    using Android.Views;
    using PokeDex.Holder;
    using Square.Picasso;
    using System.Collections.Generic;

    public class PokeDexAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;
        private readonly List<Model.PokeDex> _pokemons;

        public PokeDexAdapter(Context context, List<Model.PokeDex> pokemons)
        {
            this._context = context;
            this._pokemons = pokemons;
        }

        public override int ItemCount => this._pokemons.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PokeDexHolder poke = holder as PokeDexHolder;
            poke.TxtName.Text = this._pokemons[position].Name;
            //poke.TxtId.Text = this._pokemons[position].Id.ToString();
            var display = (this._context as Android.Support.V7.App.AppCompatActivity).WindowManager.DefaultDisplay;
            Android.Graphics.Point point = new Android.Graphics.Point();
            display.GetSize(point);
            Picasso.Get()
                   .Load(this._pokemons[position].UrlImagePrincipal)
                   .Error(this._context.GetDrawable(Resource.Drawable.whypoke))
                   .Resize(point.X, 0)
                   .Into(poke.ImgPrincipal);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(this._context).Inflate(Resource.Layout.PokeItemLayout, parent, false);
            return new PokeDexHolder(view);
        }
    }
}