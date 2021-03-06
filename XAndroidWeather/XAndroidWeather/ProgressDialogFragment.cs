namespace XAndroidWeather
{
    using Android.OS;
    using Android.Views;
    using Android.Widget;

    public class ProgressDialogFragment : Android.Support.V4.App.DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        private readonly string _status;
        public ProgressDialogFragment(string valor)
        {
            this._status = valor;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.layoutProgress, container, false);
            TextView txtview = (TextView)view.FindViewById(Resource.Id.txtoader);
            txtview.Text = this._status;
            return view;
        }
    }
}