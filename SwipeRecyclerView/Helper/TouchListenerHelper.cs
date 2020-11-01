namespace SwipeRecyclerView.Helper
{
    using Android.Graphics;
    using Android.Support.V7.Widget;
    using Android.Views;

    internal class TouchListenerHelper : Java.Lang.Object, View.IOnTouchListener
    {
        private SwipeHelper swipeHelper;

        public TouchListenerHelper(SwipeHelper swipeHelper)
        {
            this.swipeHelper = swipeHelper;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (swipeHelper.swipePosition < 0) return false;
            var point = new Point((int)e.RawX, (int)e.RawY);

            RecyclerView.ViewHolder viewHolder = swipeHelper.recyclerView.FindViewHolderForAdapterPosition(swipeHelper.swipePosition);
            View itemSwipe = viewHolder.ItemView;
            var rect = new Rect();
            itemSwipe.GetGlobalVisibleRect(rect);

            if (e.Action == MotionEventActions.Down || e.Action == MotionEventActions.Up
                || e.Action == MotionEventActions.Move)
            {
                if (rect.Top < point.Y && rect.Bottom > point.Y)
                    swipeHelper.gestureDetector.OnTouchEvent(e);
                else {
                    swipeHelper.removerQueu.Enqueue(swipeHelper.swipePosition);
                    swipeHelper.swipePosition = -1;
                    swipeHelper.RecoverySwipedItem();
                }
            }
            return false;
        }
    }
}