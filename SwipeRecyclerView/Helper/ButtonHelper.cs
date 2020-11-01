namespace SwipeRecyclerView.Helper
{
    using Android.Content;
    using Android.Content.Res;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Support.V4.Content;

    public class ButtonHelper
    {
        private int imageResId, textSize, pos;
        private string text, color;
        private RectF clickRegion;
        private ButtonClickListenerHelper listenerHelper;
        private Context context;
        private Resources resources;

        public ButtonHelper(int imageResId, int textSize,
            string text, string color,
            ButtonClickListenerHelper listenerHelper,
            Context context)
        {
            this.context = context;
            this.text = text;
            this.textSize = textSize;
            this.imageResId = imageResId;
            this.color = color;
            this.listenerHelper = listenerHelper;
            this.resources = context.Resources;
        }

        public bool OnClick(float x, float y)
        {
            if (clickRegion != null && clickRegion.Contains(x, y))
            {
                listenerHelper.OnCliCk(pos);
                return true;
            }
            return false;
        }

        public void OnDraw(Canvas canvas, RectF rectF, int pos)
        {
            var paint = new Paint();
            paint.Color = Color.ParseColor(color);
            canvas.DrawRect(rectF, paint);


            //text
            paint.Color = Color.White;
            paint.TextSize = textSize;

            var rect = new Rect();
            float cheight = rectF.Height();
            float cwidth = rectF.Width();

            paint.TextAlign = Paint.Align.Left;
            paint.GetTextBounds(text, 0, text.Length, rect);
            float x = 0, y = 0;
            if (imageResId == 0)
            {
                x = cwidth / 2f - rect.Width() / 2f - rect.Left;
                y = cheight / 2f + rect.Height() / 2f - rect.Bottom;
                canvas.DrawText(text, rectF.Left + x, rectF.Top + y, paint);
            }
            else
            {
                Drawable imageDrawable = ContextCompat.GetDrawable(context, imageResId);
                Bitmap imageBitmap = DrawableToBitmap(imageDrawable);
                canvas.DrawBitmap(imageBitmap,
                    (rectF.Left + rectF.Right) / 2,
                    (rectF.Top + rectF.Bottom) / 2,
                    paint);
            }
            clickRegion = rectF;
            this.pos = pos;
        }

        private Bitmap DrawableToBitmap(Drawable imageDrawable)
        {
            if (imageDrawable is BitmapDrawable)
                return (imageDrawable as BitmapDrawable).Bitmap;

            Bitmap bitmap = Bitmap.CreateBitmap(imageDrawable.IntrinsicWidth,
                imageDrawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            imageDrawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            imageDrawable.Draw(canvas);
            return bitmap;
        }
    }
}