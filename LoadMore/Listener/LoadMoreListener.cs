namespace LoadMore.Listener
{
    using Android.Support.V7.Widget;

    public class LoadMoreListener : RecyclerView.OnScrollListener
    {
        private MainActivity mainActivity;

        public LoadMoreListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            var itemItem = mainActivity.gridLayoutManager.ItemCount;
            var lastItemVisible = mainActivity.gridLayoutManager.FindLastVisibleItemPosition() + 1;
            if (lastItemVisible >= itemItem) mainActivity.GetMoreData();
        }
    }
}