namespace SwipeRecyclerView.Helper
{
    using Android.Views;

    internal class GestureListenerHelper : GestureDetector.SimpleOnGestureListener
    {
        private SwipeHelper swipeHelper;

        public GestureListenerHelper(SwipeHelper swipeHelper)
        {
            this.swipeHelper = swipeHelper;
        }

        public override bool OnSingleTapUp(MotionEvent e)
        {
            foreach (var item in swipeHelper.buttonHelpers)
            {
                if (item.OnClick(e.GetX(), e.GetY())) break;
            }
            return true;
        }
    }
}