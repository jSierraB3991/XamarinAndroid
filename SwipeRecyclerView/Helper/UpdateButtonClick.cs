namespace SwipeRecyclerView.Helper
{
    using Android.Widget;

    internal class UpdateButtonClick : ButtonClickListenerHelper
    {
        private ItemSwipeHelper itemSwipeHelper;

        public UpdateButtonClick(ItemSwipeHelper itemSwipeHelper)
        {
            this.itemSwipeHelper = itemSwipeHelper;
        }

        public void OnCliCk(int position)
        {
            Toast.MakeText(itemSwipeHelper.context, $"Update Pie {position}", ToastLength.Short).Show();
        }
    }
}