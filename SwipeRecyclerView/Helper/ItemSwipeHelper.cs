using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace SwipeRecyclerView.Helper
{
    class ItemSwipeHelper : SwipeHelper
    {
        public Context context;

        public ItemSwipeHelper(Context context, RecyclerView recyclerView, int buttonWidth) 
            : base(context, recyclerView, buttonWidth)
        {
            this.context = context;
        }

        public override void InstantianteButtonHolder(RecyclerView.ViewHolder viewHolder, List<ButtonHelper> buttonBuffer)
        {
            buttonBuffer.Add(new ButtonHelper(context: context, text: "Delete", 
                textSize: 30, imageResId: 0, color: "#FF9502",
                listenerHelper: new DeleteButtonClick(this)));


            buttonBuffer.Add(new ButtonHelper(context: context, text: "Update",
                textSize: 30, imageResId: 0, color: "#FF3C30",
                listenerHelper: new UpdateButtonClick(this)));
        }
    }
}