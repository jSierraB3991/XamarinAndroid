namespace ImageSlider
{
    using Android.Content;
    using Android.Support.V4.View;
    using Android.Views;
    using Android.Widget;

    public class ImageAdapter : PagerAdapter
    {
        private Context _context;
        private int[] _imageList = {
            Resource.Drawable.flower1,
            Resource.Drawable.flower2,
            Resource.Drawable.flower3,
        };

        public ImageAdapter(Context context)
        {
            this._context = context;
        }

        public override int Count => _imageList.Length;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == ((ImageView)@object);
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            var image = new ImageView(_context);
            image.SetScaleType(ImageView.ScaleType.CenterCrop);
            image.SetImageResource(_imageList[position]);
            ((ViewPager)container).AddView(image, 0);
            return image;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object @object)
        {
            ((ViewPager)container).RemoveView((ImageView)@object);
        }
    }
}