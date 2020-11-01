namespace SwipeRecyclerView.Helper
{
    using Android.Widget;

    internal class DeleteButtonClick : ButtonClickListenerHelper
    {
        private ItemSwipeHelper itemSwipeHelper;

        public DeleteButtonClick(ItemSwipeHelper itemSwipeHelper)
        {
            this.itemSwipeHelper = itemSwipeHelper;
        }

        public void OnCliCk(int position)
        {
            Toast.MakeText(itemSwipeHelper.context, $"Delete Click pie {position}", ToastLength.Short).Show();
        }
    }
}