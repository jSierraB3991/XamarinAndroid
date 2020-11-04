namespace ExpandibleListView
{
    using Android.Content;
    using Android.Views;
    using Android.Widget;
    using System.Collections.Generic;

    public class ExpandableListAdapter : BaseExpandableListAdapter
    {
        private readonly Context _context;
        private readonly List<string> _groups;
        private readonly Dictionary<string, List<string>> _lstChild;

        public ExpandableListAdapter(Context context, List<string> groups,
            Dictionary<string, List<string>> lstChild)
        {
            this._context = context;
            this._groups = groups;
            this._lstChild = lstChild;
        }

        public override int GroupCount => this._groups.Count;

        public override bool HasStableIds => false;

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            var result = new List<string>();
            _lstChild.TryGetValue(_groups[groupPosition], out result);
            return result[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition) => childPosition;

        public override int GetChildrenCount(int groupPosition)
        {
            var result = new List<string>();
            _lstChild.TryGetValue(_groups[groupPosition], out result);
            return result.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                var layoutInflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
                convertView = layoutInflater.Inflate(Resource.Layout.item_layout, null);
            }
            var textItem = convertView.FindViewById<TextView>(Resource.Id.item);
            textItem.Text = GetChild(groupPosition, childPosition).ToString();
            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition) => _groups[groupPosition];

        public override long GetGroupId(int groupPosition) => groupPosition;

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                var layoutInflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
                convertView = layoutInflater.Inflate(Resource.Layout.group_item, null);
            }
            var textItem = convertView.FindViewById<TextView>(Resource.Id.group);
            textItem.Text = GetGroup(groupPosition).ToString();
            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition) => true;
    }
}