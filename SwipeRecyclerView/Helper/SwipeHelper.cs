namespace SwipeRecyclerView.Helper
{
    using Android.Content;
    using Android.Graphics;
    using Android.Support.V7.Widget;
    using Android.Support.V7.Widget.Helper;
    using Android.Views;
    using System.Collections.Generic;

    public abstract class SwipeHelper : ItemTouchHelper.SimpleCallback
    {
        internal int buttonWidth, swipePosition = -1;
        float swipeTherhold = 0.5f;
        Dictionary<int, List<ButtonHelper>> buttonBuffer;
        internal Queue<int> removerQueu = new Queue<int>();
        GestureDetector.SimpleOnGestureListener simpleOnGestureListener;
        View.IOnTouchListener onTouchListener;
        internal RecyclerView recyclerView;
        internal List<ButtonHelper> buttonHelpers;
        internal GestureDetector gestureDetector;

        public abstract void InstantianteButtonHolder(RecyclerView.ViewHolder viewHolder,
            List<ButtonHelper> buttonBuffer);

        public SwipeHelper(Context context, RecyclerView recyclerView, int buttonWidth) : base(0, ItemTouchHelper.Left)
        {
            this.recyclerView = recyclerView;
            this.buttonHelpers = new List<ButtonHelper>();
            this.buttonBuffer = new Dictionary<int, List<ButtonHelper>>();
            this.buttonWidth = buttonWidth;

            simpleOnGestureListener = new GestureListenerHelper(this);
            onTouchListener = new TouchListenerHelper(this);
            this.gestureDetector = new GestureDetector(context, simpleOnGestureListener);
            this.recyclerView.SetOnTouchListener(onTouchListener);
            AttachSwipe();
        }

        private void AttachSwipe()
        {
            var itemTouchHelper = new ItemTouchHelper(this);
            itemTouchHelper.AttachToRecyclerView(recyclerView);
        }

        internal void RecoverySwipedItem()
        {
            while (removerQueu.Count > 0)
            {
                int pos = removerQueu.Dequeue();
                if (pos > -1)
                    recyclerView.GetAdapter().NotifyItemChanged(pos);
            }
        }

        public override bool OnMove(RecyclerView recyclerView,
            RecyclerView.ViewHolder viewHolder,
            RecyclerView.ViewHolder target) => false;

        public override float GetSwipeThreshold(RecyclerView.ViewHolder viewHolder) => swipeTherhold;

        public override float GetSwipeEscapeVelocity(float defaultValue) => 0.1f * defaultValue;

        public override float GetSwipeVelocityThreshold(float defaultValue) => 0.5f * defaultValue;

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            int pos = viewHolder.AdapterPosition;
            if (swipePosition != pos)
            {
                if (!removerQueu.Contains(swipePosition))
                    removerQueu.Enqueue(swipePosition);
                swipePosition = pos;
                if (buttonBuffer.ContainsKey(swipePosition))
                    buttonHelpers = buttonBuffer[swipePosition];
                else
                    buttonHelpers.Clear();
                buttonBuffer.Clear();
                swipeTherhold = 0.5f * buttonHelpers.Count * buttonWidth;
                RecoverySwipedItem();
            }
        }

        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            int pos = viewHolder.AdapterPosition;
            float translationX = dX;
            View itemView = viewHolder.ItemView;
            if (pos < 0)
            {
                swipePosition = pos;
                return;
            }
            if (actionState == ItemTouchHelper.ActionStateSwipe)
            {
                if (dX < 0)
                {
                    List<ButtonHelper> buffer = new List<ButtonHelper>();
                    if (!buttonBuffer.ContainsKey(pos))
                    {
                        InstantianteButtonHolder(viewHolder, buffer);
                        buttonBuffer.Add(pos, buffer);
                    }
                    else
                        buffer = buttonBuffer[pos];

                    translationX = dX * buffer.Count * buttonWidth / itemView.Width;
                    DrawButton(c, itemView, buffer, pos, translationX);
                }
            }

            base.OnChildDraw(c, recyclerView, viewHolder, translationX, dY, actionState, isCurrentlyActive);
        }

        private void DrawButton(Canvas canvas, View itemView, List<ButtonHelper> buffer, int pos, float translationX)
        {
            float right = itemView.Right;
            float dButtonWidth = -1 * translationX / buffer.Count;
            foreach (var item in buffer)
            {
                float left = right - dButtonWidth;
                item.OnDraw(canvas, new RectF(left, itemView.Top, right, itemView.Bottom), pos);
                right = left;
            }
        }
    }
}