namespace InfiniteCycleView.Adapter
{
    using Android.Content;
    using Android.Support.V4.View;
    using Android.Views;
    using Android.Widget;
    using Square.Picasso;
    using System.Collections.Generic;

    internal class AnimePagerAdapter : PagerAdapter
    {
        private readonly Context _ctx;
        private readonly List<Model.Model> _favorites;

        public AnimePagerAdapter(Context ctx, List<Model.Model> favorites)
        {
            this._ctx = ctx;
            this._favorites = favorites;
        }

        public override int Count => _favorites.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object) => view == @object;

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            container.RemoveView((View)@object);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            var view = LayoutInflater.From(_ctx)
                .Inflate(Resource.Layout.anime_item_layout, container, false);

            var image = view.FindViewById<ImageView>(Resource.Id.img_photo_title);
            var title = view.FindViewById<TextView>(Resource.Id.text_name_item);
            var description = view.FindViewById<TextView>(Resource.Id.text_item_description);
            var btnFloatin = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.btn_fav_item);

            Picasso.Get().Load(_favorites[position].ImageUrl).Into(image);
            title.Text = _favorites[position].Name;
            description.Text = _favorites[position].Description;

            view.Click += delegate { Toast.MakeText(_ctx, _favorites[position].Name, ToastLength.Short).Show(); };
            btnFloatin.Click += delegate {
                var msg = string.Format(_ctx.GetString(Resource.String.msg_click_fav), _favorites[position].Name);
                Toast.MakeText(_ctx, msg, ToastLength.Short).Show();
            };
            container.AddView(view);
            return view;
        }
    }
}